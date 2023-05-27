using Aurora.Infrastructure.Data;
using Aurora.Interfaces.Models.Reporting;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Aurora.Features.Agency.GetAgencies;

public class GetAgenciesQueryHandler : IRequestHandler<GetAgenciesQuery, GetAgenciesResponse>
{
    private readonly IReportDbContext _context;
    private readonly IMapper _mapper;

    public GetAgenciesQueryHandler(IReportDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<GetAgenciesResponse> Handle(GetAgenciesQuery request, CancellationToken cancellationToken)
    {
        var includeDeleted = request?.IncludeDeleted ?? false;
        var searchString = request?.Search ?? string.Empty;
        var agencyQuery = _context.Agencies
            .Where(_ => includeDeleted || _.DeletedOnUtc == null)
            .Where(_ => _.Name.Contains(searchString))
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
            }).OrderBy(_ => _.Name);

        var agencies = await agencyQuery.ToListAsync(cancellationToken);

        var response = new GetAgenciesResponse
        {
            Agencies = agencies
        };
        return response;
    }
}