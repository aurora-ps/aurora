﻿using Aurora.Interfaces;
using Aurora.Interfaces.Models.Reporting;
using MediatR;

namespace Aurora.Features.Agency.GetAgencies;

public class GetAgenciesQueryHandler : IRequestHandler<GetAgenciesQuery, GetAgenciesResponse>
{
    private readonly IClusterClient _clusterClient;

    public GetAgenciesQueryHandler(IClusterClient clusterClient)
    {
        _clusterClient = clusterClient;
    }

    public async Task<GetAgenciesResponse> Handle(GetAgenciesQuery request, CancellationToken cancellationToken)
    {
        var managementGrain = _clusterClient.GetGrain<IAgencyManagementGrain>("");

        var agencies = await managementGrain.GetAgenciesAsync(request.Search, request.IncludeDeleted);

        var response = new GetAgenciesResponse
        {
            Agencies = agencies ?? new List<AgencyRecord>()
        };
        return response;
    }
}