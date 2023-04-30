using Aurora.Grains.Services;
using Aurora.Interfaces;
using Microsoft.Extensions.Logging;

namespace Aurora.Grains;

public class ReportServiceGrain : Grain, IReportServiceGrain
{
    private readonly ILogger<ReportServiceGrain> _logger;
    private readonly IReportDataService _reportDataService;

    public ReportServiceGrain(IReportDataService reportDataService, ILogger<ReportServiceGrain> logger)
    {
        _reportDataService = reportDataService;
        _logger = logger;
    }

    public async Task<bool> ReportExistsAsync(string reportId)
    {
        return await _reportDataService.ExistsAsync(reportId);
    }
}

public class UserServiceGrain : Grain, IUserServiceGrain
{
    private readonly ILogger<UserServiceGrain> _logger;
    private readonly IUserDataService _userDataService;

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