using FluentValidation.Results;

namespace Aurora.Features.Agency.UnDeleteAgency;

public class UnDeleteAgencyResponse
{
    private UnDeleteAgencyResponse(bool success)
    {
        Success = success;
    }

    private UnDeleteAgencyResponse(List<ValidationFailure> validationResultErrors)
    {
        Success = false;
        ValidationResultErrors = validationResultErrors;
    }

    public List<ValidationFailure>? ValidationResultErrors { get; }

    public bool Success { get; }

    public static UnDeleteAgencyResponse ValidationFailure(List<ValidationFailure> validationResultErrors)
    {
        return new UnDeleteAgencyResponse(validationResultErrors);
    }

    public static UnDeleteAgencyResponse UnDeleted()
    {
        return new UnDeleteAgencyResponse(true);
    }
}