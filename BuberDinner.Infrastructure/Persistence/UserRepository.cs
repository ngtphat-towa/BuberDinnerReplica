using BuberDinner.Application.Persistence;
using BuberDinner.Domain.User;

namespace BuberDinner.Infrastructure.Persistence;

public class UserRepository : IUserRepository
{
    private static readonly List<User> _users = new();

    public async Task<User?> GetUserByEmail(string email)
    {
        await Task.CompletedTask;
        return _users.SingleOrDefault(u => u.Email == email);
    }

    public async Task Add(User user)
    {
        await Task.CompletedTask;

        _users.Add(user);
    }
}