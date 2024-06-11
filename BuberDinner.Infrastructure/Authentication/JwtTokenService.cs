using BuberDinner.Application.Authentication;

namespace BuberDinner.Infrastructure.Authentication;

public class JwtTokenService : IJwtTokenService
{
    public string GenerateToken(Guid userId, string firstName, string lastName)
    {
        throw new NotImplementedException();
    }
}