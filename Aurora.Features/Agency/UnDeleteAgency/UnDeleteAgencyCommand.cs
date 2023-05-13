using MediatR;

namespace Aurora.Features.Agency.UnDeleteAgency;

public class UnDeleteAgencyCommand : IRequest<UnDeleteAgencyResponse>
{
    private UnDeleteAgencyCommand(string agencyId)
    {
        AgencyId = agencyId;
    }

    public string AgencyId { get; }

    public static UnDeleteAgencyCommand Create(string agencyId)
    {
        return new UnDeleteAgencyCommand(agencyId);
    }
}