using Aurora.Interfaces;
using Aurora.Interfaces.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Aurora.Grains.Services;

/// <summary>
///     No longer a simple in-memory service - Fully functioning.
/// </summary>
public sealed class UserDataService : IUserDataService
{
    private readonly UserManager<AuroraUser> _userManager;

    public UserDataService(UserManager<AuroraUser> userManager)
    {
        _userManager = userManager;
    }

    public async Task<UserRecord?> AddAsync(UserRecord data)
    {
        if (_userManager.Users.Any(u => u.Id == data.Id)) throw new InvalidOperationException("User already exists.");
        if (_userManager.Users.Any(_ => _.Email == data.Email))throw new InvalidOperationException("User already exists.");

        var user = new AuroraUser
        {
            Id = data.Id,
            UserName = data.Name,
            Email = data.Email,
            FirstName = data.FirstName,
            LastName = data.LastName,
            LastLoginUtc = data.LastLoginUtc
        };

        var result = await _userManager.CreateAsync(user);
        if (!result.Succeeded)
        {
            return null;
        }

        return await GetByUserNameAsync(data.Name);
    }

    public async Task<IList<UserRecord>> GetAllAsync()
    {

        try
        {
            var results = await _userManager.Users.Select(u => new UserRecord
            {
                Id = u.Id,
                Name = u.UserName,
                Email = u.Email,
                FirstName = u.FirstName,
                LastName = u.LastName,
                LastLoginUtc = u.LastLoginUtc
            }).ToListAsync();

            return results;
        }
        catch (Exception ex)
        {

            throw;
        }
    }

    public async Task<UserRecord?> GetAsync(string key)
    {
        var user = await _userManager.FindByIdAsync(key);
        if (user == null) return null;
        return new UserRecord
        {
            Id = user.Id,
            Name = user.UserName,
            Email = user.Email,
            FirstName = user.FirstName,
            LastName = user.LastName,
            LastLoginUtc = user.LastLoginUtc
        };
    }

    public async Task<bool> ExistsAsync(string key)
    {
        var user = await _userManager.FindByIdAsync(key);
        return user != null;
    }

    public async Task<UserRecord?> GetByUserNameAsync(string userName)
    {
        var user = await _userManager.FindByNameAsync(userName);
        if (user == null) return null;
        return new UserRecord
        {
            Id = user.Id,
            Name = user.UserName,
            Email = user.Email,
            FirstName = user.FirstName,
            LastName = user.LastName,
            LastLoginUtc = user.LastLoginUtc
        };
    }

    public async Task DeleteAsync(string key)
    {
        var user = await _userManager.FindByIdAsync(key);
        if (user == null) return;

        var result = await _userManager.DeleteAsync(user);
    }

    public async Task SetLastLoginAsync(string key, DateTime utcNow)
    {
        var user = await _userManager.FindByIdAsync(key);
        if (user == null) return;
        user.LastLoginUtc = utcNow;
        await _userManager.UpdateAsync(user);
    }
}