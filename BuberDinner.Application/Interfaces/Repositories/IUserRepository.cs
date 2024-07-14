using BuberDinner.Domain.User;

namespace BuberDinner.Application.Interfaces.Repositories;

public interface IUserRepository
{
    Task<User?> GetUserByEmail(string email);
    Task Add(User user);
}