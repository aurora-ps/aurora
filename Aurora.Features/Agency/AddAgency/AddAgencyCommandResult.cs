using Aurora.Interfaces.Models.Reporting;
using FluentValidation.Results;

namespace Aurora.Features.Agency.AddAgency;

public class AddAgencyCommandResult
{
    private AddAgencyCommandResult(AgencyRecord agencyRecord)
    {
        AgencyRecord = agencyRecord;
        Success = true;
    }

    private AddAgencyCommandResult(List<ValidationFailure> validationResultErrors)
    {
        ValidationResultErrors = validationResultErrors;
        Success = false;
    }

    public AgencyRecord? AgencyRecord { get; }

    public List<ValidationFailure>? ValidationResultErrors { get; }

    public bool Success { get; }

    public static AddAgencyCommandResult Created(AgencyRecord agencyRecord)
    {
        return new AddAgencyCommandResult(agencyRecord);
    }

    public static AddAgencyCommandResult Error(List<ValidationFailure> validationResultErrors)
    {
        return new AddAgencyCommandResult(validationResultErrors);
    }
}