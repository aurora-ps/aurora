using MediatR;

namespace Aurora.Features.Agency.DeleteAgency;

public class DeleteAgencyCommand : IRequest<DeleteAgencyResponse>
{
    private DeleteAgencyCommand(string agencyId)
    {
        AgencyId = agencyId;
    }

    public string AgencyId { get; }

    public static DeleteAgencyCommand Create(string agencyId)
    {
        return new DeleteAgencyCommand(agencyId);
    }
}