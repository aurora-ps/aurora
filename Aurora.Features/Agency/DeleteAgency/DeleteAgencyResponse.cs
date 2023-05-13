using FluentValidation.Results;

namespace Aurora.Features.Agency.DeleteAgency;

public class DeleteAgencyResponse
{
    private DeleteAgencyResponse(bool success)
    {
        Success = success;
    }

    private DeleteAgencyResponse(List<ValidationFailure> validationResultErrors)
    {
        Success = false;
        ValidationResultErrors = validationResultErrors;
    }

    public List<ValidationFailure>? ValidationResultErrors { get; }

    public bool Success { get; }

    public static DeleteAgencyResponse ValidationFailure(List<ValidationFailure> validationResultErrors)
    {
        return new DeleteAgencyResponse(validationResultErrors);
    }

    public static DeleteAgencyResponse Deleted()
    {
        return new DeleteAgencyResponse(true);
    }
}