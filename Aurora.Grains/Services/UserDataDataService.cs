﻿using Aurora.Interfaces;
using Aurora.Interfaces.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Aurora.Grains.Services;

/// <summary>
///     Simple in-memory data service for testing.
/// </summary>
public sealed class UserDataDataService : IUserDataService
{
    private readonly UserManager<AuroraUser> _userManager;

    public UserDataDataService(UserManager<AuroraUser> userManager)
    {
        _userManager = userManager;
    }

    public async Task<UserRecord> AddAsync(UserRecord data)
    {
        if (_userManager.Users.Any(u => u.Id == data.Id)) throw new InvalidOperationException("User already exists.");

        var user = new AuroraUser
        {
            Id = data.Id,
            UserName = data.Name,
            Email = data.Email
        };

        var result = await _userManager.CreateAsync(user);
        if (!result.Succeeded) throw new InvalidOperationException("Failed to create user.");

        return await GetByUserNameAsync(data.Name);
    }

    public async Task<IList<UserRecord>> GetAllAsync()
    {
        var results = await _userManager.Users.Select(u => new UserRecord
        {
            Id = u.Id,
            Name = u.UserName,
            Email = u.Email
        }).ToListAsync();

        return results;
    }

    public async Task<UserRecord?> GetAsync(string key)
    {
        var user = await _userManager.FindByIdAsync(key);
        if (user == null) return null;
        return new UserRecord
        {
            Id = user.Id,
            Name = user.UserName,
            Email = user.Email
        };
    }

    public async Task<UserRecord?> GetByUserNameAsync(string userName)
    {
        var user = await _userManager.FindByNameAsync(userName);
        if (user == null) return null;
        return new UserRecord
        {
            Id = user.Id,
            Name = user.UserName,
            Email = user.Email
        };
    }

    public async Task DeleteAsync(string key)
    {
        var user = await _userManager.FindByIdAsync(key);
        if (user == null) return;

        var result = await _userManager.DeleteAsync(user);
    }
}