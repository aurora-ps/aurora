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
                        .Include(a => a.IncidentTypes)
                        .Include("IncidentTypes.IncidentType")
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

    public class GetAgenciesQuery : IRequest<GetAgenciesResponse>
    {
    }

    public class GetAgenciesResponse
    {
        public IList<Interfaces.Models.Reporting.Agency> Agencies { get; set; } = new List<Interfaces.Models.Reporting.Agency>();
    }
}
