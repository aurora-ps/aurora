using Aurora.Infrastructure.Data;
using Aurora.Interfaces.Models.Reporting;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Aurora.Features.Agency.GetAgency;

public class GetAgencyHandler : IRequestHandler<GetAgencyQuery, GetAgencyResult>
{
    private readonly IReportDbContext _context;

    public GetAgencyHandler(IReportDbContext context)
    {
        _context = context;
    }

    public async Task<GetAgencyResult> Handle(GetAgencyQuery request, CancellationToken cancellationToken)
    {
        // validate the command
        var validator = new GetAgencyQueryValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);
        // if the command is not valid, return not found
        if (!validationResult.IsValid)
            return GetAgencyResult.NotFound();

        // Get the agency
        var agency = await _context.Agencies
            .Select(a => new AgencyRecord
            {
                Id = a.Id,
                Name = a.Name,
                DeletedOnUtc = a.DeletedOnUtc,
                IncidentTypes = a.IncidentTypes.Select(it => new IncidentTypeRecord
                {
                    Id = it.IncidentType.Id,
                    Name = it.IncidentType.Name,
                    CollectLocation = it.CollectLocation ?? it.IncidentType.CollectLocation,
                    CollectPerson = it.CollectPerson ?? it.IncidentType.CollectPerson,
                    CollectTime = it.CollectTime ?? it.IncidentType.CollectTime,
                    RequiresTime = it.RequiresTime ?? it.IncidentType.RequiresTime,
                    ShowGospelPresentations = it.ShowGospelPresentations ?? it.IncidentType.ShowGospelPresentations,
                    ShowProfessionsOfFaith = it.ShowProfessionsOfFaith ?? it.IncidentType.ShowProfessionsOfFaith,
                    ShowBaptisms = it.ShowBaptisms ?? it.IncidentType.ShowBaptisms,
                    ShowBibleStudies = it.ShowBibleStudies ?? it.IncidentType.ShowBibleStudies,
                    ShowCounselingOpportunities =
                        it.ShowCounselingOpportunities ?? it.IncidentType.ShowCounselingOpportunities
                }).OrderBy(_ => _.Name).ToList()
            }).FirstOrDefaultAsync(_ => _.Id.Equals(request.AgencyId), cancellationToken);

        // get the agency grain
        if (agency == null)
            return GetAgencyResult.NotFound();

        return GetAgencyResult.Success(agency);
    }
}