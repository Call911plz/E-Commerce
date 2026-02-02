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

    public List<UserInfo> GetAllUsers()
    {
        return _context.Users.ToList();
    }

    public UserInfo? GetUserInfo(int id)
    {
        return _context.Users.Find(id);
    }

    public async Task<UserInfo> UpdateUserAsync(UserInfo info)
    {
        // Throwing error since user should have been checked before hand.
        var user =
            await _context.Users.FindAsync(info.Id)
            ?? throw new Exception("Invalid User id provided");

        user.Username = info.Username;
        user.Password = info.Password;

        await _context.SaveChangesAsync();

        return user;
    }

    public async Task<UserInfo> DeleteUserAsync(int id)
    {
        var user =
            await _context.Users.FindAsync(id) ?? throw new Exception("Invalid User id provided");

        _context.Remove(user);

        await _context.SaveChangesAsync();

        return user;
    }
}
