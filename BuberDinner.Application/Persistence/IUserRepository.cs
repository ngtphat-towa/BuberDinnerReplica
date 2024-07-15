using BuberDinner.Domain.Entities;

namespace BuberDinner.Application.Persistence;

public interface IUserRepository
{
    Task<User?> GetUserByEmail(string email);
    Task Add(User user);
}