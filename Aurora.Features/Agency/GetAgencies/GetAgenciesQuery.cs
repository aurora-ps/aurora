using MediatR;

namespace Aurora.Features.Agency.GetAgencies;

public class GetAgenciesQuery : IRequest<GetAgenciesResponse>
{
    public string? Search { get; set; }

    public bool? IncludeDeleted { get; set; }
}