using BuberDinner.Domain.User;

namespace BuberDinner.Application.Common.Services;
public interface IJwtTokenGenerator
{
    string GenerateToken(User user);
}