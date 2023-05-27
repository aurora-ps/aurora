using Aurora.Interfaces;
using MediatR;

namespace Aurora.Features.User.AddUser;

public class AddUserCommandHandler : IRequestHandler<AddUserCommand, AddUserResponse>
{
    private readonly IClusterClient _clusterClient;

    public AddUserCommandHandler(IClusterClient clusterClient)
    {
        _clusterClient = clusterClient;
    }

    public async Task<AddUserResponse> Handle(AddUserCommand command, CancellationToken cancellationToken)
    {
        var grain = _clusterClient.GetGrain<IUserServiceGrain>(Guid.NewGuid().ToString());

        var newUser = new UserRecord
        {
            Id = Guid.NewGuid().ToString(),
            Email = command.UserName,
            Name = command.UserName,
            FirstName = command.FirstName,
            LastName = command.LastName
        };

        var userRecord = await grain.AddAsync(newUser);
        if (userRecord is null)
            return AddUserResponse.CreateFailure();

        return AddUserResponse.CreateSuccess(userRecord);
    }
}