using Aurora.Infrastructure.Data;
using Aurora.Interfaces.Models.Reporting;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Aurora.Features.Agency.GetAgencies
{
    public class GetAgenciesQueryHandler : IRequestHandler<GetAgenciesQuery, GetAgenciesResponse>
    {
        private readonly ReportDbContext _context;

        public GetAgenciesQueryHandler(ReportDbContext context)
        {
            _context = context;
        }
        public async Task<GetAgenciesResponse> Handle(GetAgenciesQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var agencies = await _context.Agencies
                    .Select(a => new AgencyRecord
                    {
                        Id = a.Id,
                        Name = a.Name,
                        IncidentTypes = a.IncidentTypes.Select(it => new IncidentTypeRecord
                        {
                            Id = it.IncidentType.Id,
                            Name = it.IncidentType.Name,
                            CollectLocation = it.IncidentType.CollectLocation,
                            CollectPerson = it.IncidentType.CollectPerson,
                            CollectTime = it.IncidentType.CollectTime,
                            RequiresTime = it.IncidentType.RequiresTime,

                        }).ToList()
                    })
                .ToListAsync(cancellationToken);

                var response = new GetAgenciesResponse()
                {
                    Agencies = agencies
                };
                return response;
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}
