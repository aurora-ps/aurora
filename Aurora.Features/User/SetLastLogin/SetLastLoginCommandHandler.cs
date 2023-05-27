using Aurora.Infrastructure.Data;
using FluentValidation;
using MediatR;

namespace Aurora.Features.User.SetLastLogin;

public class SetLastLoginCommandHandler : IRequestHandler<SetLastLoginCommand>
{
    private readonly IApplicationDbContext _context;
    private readonly IValidator<SetLastLoginCommand> _validator;

    public SetLastLoginCommandHandler(IApplicationDbContext context, IValidator<SetLastLoginCommand> validator)
    {
        _context = context;
        _validator = validator;
    }

    public Task Handle(SetLastLoginCommand command, CancellationToken cancellationToken)
    {
        var validationResult = _validator.Validate(command);
        if (!validationResult.IsValid)
            return Task.FromException(new ValidationException(validationResult.Errors));

        var user = _context.Users.FirstOrDefault(_ => _.Id == command.UserId);
        if (user is null)
            return Task.FromException(new KeyNotFoundException($"User with id {command.UserId} not found"));

        user.LastLoginUtc = command.LastLogin;
        _context.Users.Update(user);
        return _context.SaveChangesAsync(cancellationToken);
    }
}

// add validator