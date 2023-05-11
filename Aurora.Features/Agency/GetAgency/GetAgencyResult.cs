using Aurora.Interfaces.Models.Reporting;

namespace Aurora.Features.Agency.GetAgency;

public class GetAgencyResult
{
    private GetAgencyResult()
    {
    }

    private GetAgencyResult(AgencyRecord agency)
    {
        Agency = agency;
    }

    public AgencyRecord? Agency { get; set; }

    public static GetAgencyResult Success(AgencyRecord agency)
    {
        return new GetAgencyResult(agency);
    }

    public static GetAgencyResult NotFound()
    {
        return new GetAgencyResult();
    }
}