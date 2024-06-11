namespace BuberDinner.Application.Common.Interfaces;

public interface IJwtTokenService
{
    string GenerateToken(Guid userId, string firstName, string lastName);
}