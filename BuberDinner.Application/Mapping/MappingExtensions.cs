using BuberDinner.Domain.Entities;

namespace BuberDinner.Application.Mapping;

public static class MappingExtensions
{
    public static AuthenticationResult MapUserEntityToResult(User user, string token)
    {
        return new AuthenticationResult(
                   user,
                   token);
    }
}