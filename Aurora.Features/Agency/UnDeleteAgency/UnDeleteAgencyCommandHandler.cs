using Aurora.Infrastructure.Data;
using FluentValidation.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Aurora.Features.Agency.UnDeleteAgency;

public class UnDeleteAgencyCommandHandler : IRequestHandler<UnDeleteAgencyCommand, UnDeleteAgencyResponse>
{
    private readonly IReportDbContext _context;

    public UnDeleteAgencyCommandHandler(IReportDbContext context)
    {
        _context = context;
    }

    public async Task<UnDeleteAgencyResponse> Handle(UnDeleteAgencyCommand command, CancellationToken cancellationToken)
    {
        // validate command
        var validator = new UnDeleteAgencyValidator();
        var validationResult = await validator.ValidateAsync(command, cancellationToken);
        if (!validationResult.IsValid) return UnDeleteAgencyResponse.ValidationFailure(validationResult.Errors);

        // get agency
        var agency = await _context.Agencies
            .FirstOrDefaultAsync(_ => _.Id.Equals(command.AgencyId), cancellationToken);

        if (agency == null)
            return UnDeleteAgencyResponse.ValidationFailure(new List<ValidationFailure>
            {
                new(nameof(command.AgencyId), $"Agency with id {command.AgencyId} not found")
            });

        if (agency.DeletedOnUtc == null)
            return UnDeleteAgencyResponse.ValidationFailure(new List<ValidationFailure>
            {
                new(nameof(command.AgencyId), $"Agency with id {command.AgencyId} is not deleted")
            });

        agency.DeletedOnUtc = null;
        await _context.SaveChangesAsync(cancellationToken);

        return UnDeleteAgencyResponse.UnDeleted();
    }
}