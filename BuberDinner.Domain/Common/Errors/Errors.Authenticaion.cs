using ErrorOr;

namespace BuberDinner.Domain.Common.Errors;

public static partial class DomainErrors
{
    public static class Auth
    {
        public static Error InvalidCredentials => Error.Conflict(
              code: "Auth.InvalidCredential",
              description: "Invalid credentials"
          );
    }

}