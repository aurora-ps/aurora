using Aurora.Infrastructure.Data;
using Aurora.Interfaces;
using FluentValidation.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Aurora.Features.Agency.UpdateIncidentTypeOverrides;

public class
    UpdateIncidentTypeOverridesHandler : IRequestHandler<UpdateIncidentTypeOverridesCommand,
        UpdateIncidentTypeOverridesResult>
{
    private readonly IReportDbContext _context;

    public UpdateIncidentTypeOverridesHandler(IReportDbContext context)
    {
        _context = context;
    }

    public async Task<UpdateIncidentTypeOverridesResult> Handle(UpdateIncidentTypeOverridesCommand command,
        CancellationToken cancellationToken)
    {
        // validate the command
        var validator = new UpdateIncidentTypeOverridesCommandValidator();
        var validationResult = await validator.ValidateAsync(command, cancellationToken);

        // if the command is not valid, return the validation errors
        if (!validationResult.IsValid)
            return UpdateIncidentTypeOverridesResult.Create(validationResult.Errors);

        // get the agency
        var agency = await _context.Agencies.Include(_ => _.IncidentTypes)
            .FirstOrDefaultAsync(_ => _.Id == command.AgencyId, cancellationToken);

        if (agency == null)
            return UpdateIncidentTypeOverridesResult.Create(new List<ValidationFailure>
                               { new("Id", "Agency not found") });


        var agencyIncidentType = agency.IncidentTypes.FirstOrDefault(_ => _.IncidentTypeId == command.IncidentType!.Id);

        if (agencyIncidentType == null)
            return UpdateIncidentTypeOverridesResult.Create(new List<ValidationFailure>
                { new("Id", "Incident type not found") });

        // get the agency incident type
        agencyIncidentType.CollectLocation = command.IncidentType!.CollectLocation;
        agencyIncidentType.CollectTime = command.IncidentType!.CollectTime;
        agencyIncidentType.RequiresTime= command.IncidentType!.RequiresTime;
        agencyIncidentType.CollectLocation = command.IncidentType!.CollectLocation;
        agencyIncidentType.CollectPerson = command.IncidentType!.CollectPerson;
        agencyIncidentType.ShowGospelPresentations = command.IncidentType!.ShowGospelPresentations;
        agencyIncidentType.ShowProfessionsOfFaith = command.IncidentType!.ShowProfessionsOfFaith;
        agencyIncidentType.ShowBaptisms = command.IncidentType!.ShowBaptisms;
        agencyIncidentType.ShowBibleStudies = command.IncidentType!.ShowBibleStudies;
        agencyIncidentType.ShowCounselingOpportunities = command.IncidentType!.ShowCounselingOpportunities;
        
        // save changes
        await _context.SaveChangesAsync(cancellationToken);

        return UpdateIncidentTypeOverridesResult.CreateSuccess();
    }
}