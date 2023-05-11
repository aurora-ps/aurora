using FluentValidation.Results;

namespace Aurora.Features.Agency.UpdateIncidentTypeOverrides;

public class UpdateIncidentTypeOverridesResult
{
    public bool Success { get; set; }
    public IList<ValidationFailure>? Errors { get; set; }

    public static UpdateIncidentTypeOverridesResult CreateSuccess()
    {
        return new UpdateIncidentTypeOverridesResult
        {
            Success = true
        };
    }

    public static UpdateIncidentTypeOverridesResult Create(IList<ValidationFailure> errors)
    {
        return new UpdateIncidentTypeOverridesResult
        {
            Success = false,
            Errors = errors
        };
    }
}