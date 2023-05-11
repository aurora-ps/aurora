using MediatR;

namespace Aurora.Features.Agency.GetAgency;

public class GetAgencyQuery : IRequest<GetAgencyResult>
{
    public string? AgencyId { get; set; }
}