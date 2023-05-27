using Aurora.Infrastructure.Data;
using Aurora.Interfaces.Models.Reporting;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Aurora.Features.Agency.AddAgency;

public class AddAgencyCommandHandler : IRequestHandler<AddAgencyCommand>
{
    private readonly ReportDbContext _context;

    public AddAgencyCommandHandler(ReportDbContext context)
    {
        _context = context;
    }

    public async Task Handle(AddAgencyCommand command, CancellationToken cancellationToken)
    {
        // Validate command
        var validator = new AddAgencyCommandValidator();
        var validationResult = await validator.ValidateAsync(command, cancellationToken);

        if (!validationResult.IsValid)
        {
            var exception = validationResult.Errors.FirstOrDefault();
            throw new ArgumentException(exception!.ErrorMessage, nameof(command));
        }

        if (await AgencyExists(command.Name))
            throw new InvalidOperationException("Agency already exists");


        try
        {
            var newAgency = new Interfaces.Models.Reporting.Agency(command.Id, command.Name)
            {
                IncidentTypes = new List<AgencyIncidentType>()
            };

            await _context.Agencies.AddAsync(newAgency, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("Agency could not be created");
        }
    }

    private async Task<bool> AgencyExists(string agencyName)
    {
        return await _context.Agencies.AnyAsync(_ => _.Name.Trim() == agencyName.Trim());
    }
}