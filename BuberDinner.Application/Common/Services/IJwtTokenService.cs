using BuberDinner.Domain.Entities;

namespace BuberDinner.Application.Common.Services;
public interface IJwtTokenGenerator
{
    string GenerateToken(User user);
}