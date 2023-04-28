using Aurora.Interfaces;
using MediatR;

namespace Aurora.Features.User;

public class GetUserQueryHandler : IRequestHandler<GetUserQuery, GetUserResponse>
{
    private readonly IClusterClient _clusterClient;

    public GetUserQueryHandler(IClusterClient clusterClient)
    {
        _clusterClient = clusterClient;
    }

    public async Task<GetUserResponse> Handle(GetUserQuery request, CancellationToken cancellationToken)
    {
        var userGrain = _clusterClient.GetGrain<IUserGrain>(request.UserId);
        var user = await userGrain.GetDetailsAsync();
        if (user is null)
            return new GetUserResponse { Success = false };

        return GetUserResponse.CreateSuccess(user);
    }
}