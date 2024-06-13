using ErrorOr;

namespace BuberDinner.Domain.Common.Errors;

public static partial class Errors
{
    public static class Auth
    {
        public static Error InvalidCredential => Error.Conflict(
              code: "Auth.InvalidCredential",
              description: "Invalid credentials"
          );
    }

}