using FluentValidation.Results;

namespace Aurora.Features.Agency.AddIncidentType;

public class AddAgencyIncidentTypeResult
{
    private AddAgencyIncidentTypeResult(bool success)
    {
        Success = success;
    }

    private AddAgencyIncidentTypeResult(bool success, List<ValidationFailure> validationErrors)
    {
        Success = success;
        ValidationErrors = validationErrors;
    }

    public List<ValidationFailure>? ValidationErrors { get; set; }

    public bool Success { get; set; }

    public static AddAgencyIncidentTypeResult Create(List<ValidationFailure> validationResultErrors)
    {
        return new AddAgencyIncidentTypeResult(false, validationResultErrors);
    }

    public static AddAgencyIncidentTypeResult CreateSuccess()
    {
        return new AddAgencyIncidentTypeResult(true);
    }
}