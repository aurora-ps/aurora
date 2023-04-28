using Aurora.Interfaces;
using MediatR;

namespace Aurora.Features.User;

public class GetUsersQueryHandler : IRequestHandler<GetUsersQuery, GetUsersResponse>
{
    private readonly IClusterClient _clusterClient;

    public GetUsersQueryHandler(IClusterClient clusterClient)
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