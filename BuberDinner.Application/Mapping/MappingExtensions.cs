using BuberDinner.Application.Features.Authentication.Models;
using BuberDinner.Domain.User;

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