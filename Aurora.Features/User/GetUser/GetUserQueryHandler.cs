using Aurora.Interfaces;
using FluentValidation;
using MediatR;

namespace Aurora.Features.User.GetUser;

public class GetUserQueryHandler : IRequestHandler<GetUserQuery, GetUserResponse>
{
    private readonly IClusterClient _clusterClient;
    private readonly IValidator<GetUserQuery> _validator;

    public GetUserQueryHandler(IValidator<GetUserQuery> validator, IClusterClient clusterClient)
    {
        _validator = validator;
        _clusterClient = clusterClient;
    }

    public async Task<GetUserResponse> Handle(GetUserQuery request, CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(request);
        if (!validationResult.IsValid)
            return GetUserResponse.CreateFailure(validationResult.Errors);

        var userGrain = _clusterClient.GetGrain<IUserGrain>(request.UserId);
        var user = await userGrain.GetDetailsAsync();
        if (user is null)
            return new GetUserResponse { Success = false };

        return GetUserResponse.CreateSuccess(user);
    }
}