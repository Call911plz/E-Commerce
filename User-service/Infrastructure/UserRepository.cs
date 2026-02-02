using Microsoft.EntityFrameworkCore;
using UserMicroService.Application;
using UserMicroService.Domain;

namespace UserMicroService.Infrastructure;

public class UserRepository(UserDbContext context) : IUserRepository
{
    private readonly UserDbContext _context = context;

    public async Task<UserInfo> CreateUserAsync(UserInfo info)
    {
        var result = _context.Users.Add(info);

        await _context.SaveChangesAsync();

        return result.Entity;
    }

    public async Task<List<UserInfo>> GetAllUsersAsync()
    {
        return _context.Users.ToList();
    }

    public async Task<UserInfo?> GetUserInfoAsync(string uuid)
    {
        return await _context.Users.FirstOrDefaultAsync(user => user.Uuid == uuid);
    }

    public async Task<UserInfo> UpdateUserAsync(string uuid, UserInfo info)
    {
        // Throwing error since user should have been checked before hand.
        var user = await GetUserInfoAsync(uuid) ?? throw new Exception("Invalid User id provided");

        user.Username = info.Username;
        user.Password = info.Password;

        await _context.SaveChangesAsync();

        return user;
    }

    public async Task<UserInfo> DeleteUserAsync(string uuid)
    {
        var user = await GetUserInfoAsync(uuid) ?? throw new Exception("Invalid User id provided");

        _context.Remove(user);

        await _context.SaveChangesAsync();

        return user;
    }
}
