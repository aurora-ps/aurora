using Aurora.Interfaces.Models.Reporting;
using MediatR;

namespace Aurora.Features.Agency.GetAgencies
{
    public class GetAgenciesQueryHandler : IRequestHandler<GetAgenciesQuery, GetAgenciesResponse>
    {
        public async Task<GetAgenciesResponse> Handle(GetAgenciesQuery request, CancellationToken cancellationToken)
        {
            var agencies = new List<Interfaces.Models.Reporting.Agency>()
            {
                new ("DurhamCRT", "Durham - CRT"){IncidentTypes = GetAllIncidentTypes()},
                new ("PersonCRT", "Person - CRT"){IncidentTypes = GetAllIncidentTypes()},
            };

            var response = new GetAgenciesResponse()
            {
                Agencies = agencies
            };
            return response;
        }

        private IList<IncidentType> GetAllIncidentTypes()
        {
            return new List<IncidentType>()
            {
                new()
                {
                    Id = "DeathCall", Name = "Death Call", CollectTime = true, RequiresTime = true, CollectLocation = true,
                    CollectPerson = true
                },
                new() { Id = "Administration", Name = "Administration" },
                new() { Id = "Training", Name = "Training", CollectTime = true, RequiresTime = true, CollectLocation = true },
                new() { Id = "Other", Name = "Other", CollectTime = true },
                new()
                {
                    Id = "CrisisCall", Name = "Crisis Call", CollectTime = true, RequiresTime = true, CollectLocation = true
                },
            };
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
