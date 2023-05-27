using Aurora.Grains.Services;
using Aurora.Interfaces;
using MediatR;

namespace Aurora.Features.User.AddUser;

public class AddUserCommandHandler : IRequestHandler<AddUserCommand, AddUserResponse>
{
    private readonly IUserDataService _userDataService;

    public AddUserCommandHandler(IUserDataService userDataService)
    {
        _userDataService = userDataService;
    }

    public async Task<AddUserResponse> Handle(AddUserCommand command, CancellationToken cancellationToken)
    {
        var newUser = new UserRecord
        {
            Id = Guid.NewGuid().ToString(),
            Email = command.UserName,
            Name = command.UserName,
            FirstName = command.FirstName,
            LastName = command.LastName
        };

        var userRecord = await _userDataService.AddAsync(newUser);

        if (userRecord is null)
            return AddUserResponse.CreateFailure();

        return AddUserResponse.CreateSuccess(userRecord);
    }
}