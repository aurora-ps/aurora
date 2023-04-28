using Aurora.Interfaces;
using MediatR;

namespace Aurora.Api.Endpoints.User;

public class GetUserQueryHandler : IRequestHandler<GetUsersQuery, GetUsersResponse>
{
    private readonly IClusterClient _clusterClient;

    public GetUserQueryHandler(IClusterClient clusterClient)
    {
        _clusterClient = clusterClient;
    }

    public async Task<GetUsersResponse> Handle(GetUsersQuery request, CancellationToken cancellationToken)
    {
        var userService = _clusterClient.GetGrain<IUserServiceGrain>("");
        var users = await userService.GetAllAsync();

        return GetUsersResponse.CreateSuccess(users);
    }
}