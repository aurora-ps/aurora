using Aurora.Grains.Services;
using Aurora.Interfaces;
using Microsoft.Extensions.Logging;

namespace Aurora.Grains;

public class UserServiceGrain : Grain, IUserServiceGrain
{
    private readonly IUserDataService _userDataService;
    private readonly ILogger<UserServiceGrain> _logger;

    public UserServiceGrain(IUserDataService userDataService, ILogger<UserServiceGrain> logger)
    {
        _userDataService = userDataService;
        _logger = logger;
    }
    public Task<UserRecord?> FindByIdAsync(string userId)
    {
        return _userDataService.GetAsync(userId);
    }

    public Task<UserRecord?> FindByNameAsync(string userName)
    {
        return _userDataService.GetByUserNameAsync(userName);
    }

    public Task<IList<UserRecord>> GetAllAsync()
    {
        return _userDataService.GetAllAsync();
    }
}