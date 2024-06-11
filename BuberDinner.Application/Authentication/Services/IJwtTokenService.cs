namespace BuberDinner.Application.Authentication;

public interface IJwtTokenService
{
    string GenerateToken(Guid userId, string firstName, string lastName);
}