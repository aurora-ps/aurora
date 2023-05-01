using Aurora.Interfaces;
using Aurora.Interfaces.Models;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Aurora.Features.User.RegisterUser;

public class RegisterUserHandler : IRequestHandler<RegisterUserCommand, RegisterUserResponse>
{
    private readonly IClusterClient _clusterClient;
    private readonly IValidator<RegisterUserCommand> _registerValidator;
    private readonly UserManager<AuroraUser> _userManager;

    public RegisterUserHandler(UserManager<AuroraUser> userManager,
        IClusterClient clusterClient,
        IValidator<RegisterUserCommand> registerValidator)
    {
        _userManager = userManager;
        _clusterClient = clusterClient;
        _registerValidator = registerValidator;
    }

    public async Task<RegisterUserResponse> Handle(RegisterUserCommand command, CancellationToken cancellationToken)
    {
        var validationResult = await _registerValidator.ValidateAsync(command, cancellationToken);
        if (!validationResult.IsValid)
            return RegisterUserResponse.CreateFailure(validationResult.Errors);

        var userExists = await _userManager.FindByNameAsync(command.UserName);
        if (userExists is not null)
            return RegisterUserResponse.Conflict();

        var identityUser = new AuroraUser
        {
            UserName = command.UserName,
            Email = command.Email,
            SecurityStamp = Guid.NewGuid().ToString()
        };
        var result = await _userManager.CreateAsync(identityUser, command.Password);
        if (!result.Succeeded)
            return RegisterUserResponse.Unauthorized();

        identityUser = await _userManager.FindByNameAsync(command.UserName);

        var user = _clusterClient.GetGrain<IUserGrain>(identityUser.Id);
        var userRecord = await user.GetDetailsAsync();
        return RegisterUserResponse.Created(userRecord);
    }
}