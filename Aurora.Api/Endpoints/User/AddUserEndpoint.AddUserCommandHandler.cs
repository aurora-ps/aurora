using Aurora.Interfaces;
using MediatR;

namespace Aurora.Api.Endpoints.User;

public class AddUserCommandHandler : IRequestHandler<AddUserCommand, AddUserResponse>
{
    private readonly IClusterClient _clusterClient;

    public AddUserCommandHandler(IClusterClient clusterClient)
    {
        _clusterClient = clusterClient;
    }

    public async Task<AddUserResponse> Handle(AddUserCommand command, CancellationToken cancellationToken)
    {
        var grain = _clusterClient.GetGrain<IUserGrain>(Guid.NewGuid().ToString());
        var userRecord = await grain.AddAsync(command.UserName, command.Email);
        if (userRecord is null)
            return AddUserResponse.CreateFailure();

        return AddUserResponse.CreateSuccess(userRecord);
    }
}