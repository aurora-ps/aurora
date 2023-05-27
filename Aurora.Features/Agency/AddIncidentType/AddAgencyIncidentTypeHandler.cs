using Aurora.Infrastructure.Data;
using Aurora.Interfaces;
using Aurora.Interfaces.Models.Reporting;
using FluentValidation.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Aurora.Features.Agency.AddIncidentType;

public class AddAgencyIncidentTypeHandler : IRequestHandler<AddAgencyIncidentTypeCommand, AddAgencyIncidentTypeResult>
{
    private readonly IReportDbContext _context;

    public AddAgencyIncidentTypeHandler(IReportDbContext context)
    {
        _context = context;
    }

    public async Task<AddAgencyIncidentTypeResult> Handle(AddAgencyIncidentTypeCommand command,
        CancellationToken cancellationToken)
    {
        // validate the command
        var validator = new AddAgencyIncidentTypeCommandValidator();
        var validationResult = await validator.ValidateAsync(command, cancellationToken);

        // if the command is not valid, return the validation errors
        if (!validationResult.IsValid)
            return AddAgencyIncidentTypeResult.Create(validationResult.Errors);

        // get the agency
        var agency = await _context.Agencies.Include(_ => _.IncidentTypes).FirstOrDefaultAsync(_ => _.Id == command.AgencyId, cancellationToken);
        if (agency == null)
            return AddAgencyIncidentTypeResult.Create(new List<ValidationFailure> { new("Id", "Agency not found") });
        if(agency.DeletedOnUtc.HasValue)
            return AddAgencyIncidentTypeResult.Create(new List<ValidationFailure> { new("Id", "Agency is deleted") });
        
        if(agency.IncidentTypes.Any(_ => _.IncidentTypeId == command.IncidentType.Id))
            return AddAgencyIncidentTypeResult.Create(new List<ValidationFailure> { new("Id", "Incident type already exists") });

        var agencyType = await _context.IncidentTypes.FirstOrDefaultAsync(_ => _.Id == command.IncidentType.Id, cancellationToken);
        if (agencyType == null)
            return AddAgencyIncidentTypeResult.Create(new List<ValidationFailure> { new("Id", "Incident type not found") });

        agency.IncidentTypes.Add(new AgencyIncidentType
        {
            IncidentTypeId = agencyType.Id
        });
        
        await _context.SaveChangesAsync(cancellationToken);

        return AddAgencyIncidentTypeResult.CreateSuccess();
    }
}