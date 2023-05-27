using Aurora.Infrastructure.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Aurora.Features.Agency.RemoveIncidentType;

public class
    RemoveAgencyIncidentTypeCommandHandler : IRequestHandler<RemoveAgencyIncidentTypeCommand,
        RemoveAgencyIncidentTypeResult>
{
    private readonly IReportDbContext _context;

    public RemoveAgencyIncidentTypeCommandHandler(IReportDbContext context)
    {
        _context = context;
    }

    public async Task<RemoveAgencyIncidentTypeResult> Handle(RemoveAgencyIncidentTypeCommand command,
        CancellationToken cancellationToken)
    {
        // get agency
        var agency = await _context.Agencies
            .FirstOrDefaultAsync(_ => _.Id.Equals(command.AgencyId) && _.DeletedOnUtc == null, cancellationToken);

        if (agency == null) return RemoveAgencyIncidentTypeResult.NotFound();

        agency.DeletedOnUtc = DateTime.UtcNow;
        await _context.SaveChangesAsync(cancellationToken);

        return RemoveAgencyIncidentTypeResult.Success();
    }
}