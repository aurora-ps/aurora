using Aurora.Interfaces;
using MediatR;

namespace Aurora.Features.User.DeleteUser;

public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, DeleteUserResponse>
{
    private readonly IClusterClient _clusterClient;

    public DeleteUserCommandHandler(IClusterClient clusterClient)
    {
        _clusterClient = clusterClient;
    }

    public async Task<DeleteUserResponse> Handle(DeleteUserCommand command, CancellationToken cancellationToken)
    {
        var grain = _clusterClient.GetGrain<IUserGrain>(command.UserId);
        if (await grain.IsInitialized())
        {
            var results = await grain.DeleteAsync();
            if (results)
                return DeleteUserResponse.CreateSuccess();

            return DeleteUserResponse.CreateFailure();
        }

        return DeleteUserResponse.CreateNotFound();
    }
}