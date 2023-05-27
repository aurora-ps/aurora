using Aurora.Infrastructure.Data;
using Aurora.Interfaces;
using Aurora.Interfaces.Models;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Aurora.Features.User.RegisterUser;

public class RegisterUserHandler : IRequestHandler<RegisterUserCommand, RegisterUserResponse>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;
    private readonly IValidator<RegisterUserCommand> _registerValidator;
    private readonly UserManager<AuroraUser> _userManager;

    public RegisterUserHandler(UserManager<AuroraUser> userManager,
        IApplicationDbContext context,
        IMapper mapper,
        IValidator<RegisterUserCommand> registerValidator)
    {
        _userManager = userManager;
        _context = context;
        _mapper = mapper;
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
            FirstName = command.FirstName,
            LastName = command.LastName,
            SecurityStamp = Guid.NewGuid().ToString()
        };
        var result = await _userManager.CreateAsync(identityUser, command.Password);
        if (!result.Succeeded)
            return RegisterUserResponse.Unauthorized();

        identityUser = await _userManager.FindByNameAsync(command.UserName);

        var user = await _context.Users.ProjectTo<UserRecord>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(_ => _.Id.Equals(identityUser.Id), cancellationToken);

        return RegisterUserResponse.Created(user);
    }
}