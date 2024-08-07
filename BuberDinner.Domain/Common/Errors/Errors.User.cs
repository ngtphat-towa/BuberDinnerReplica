using ErrorOr;

namespace BuberDinner.Domain.Common.Errors;

public static partial class DomainErrors
{
    public static class User
    {
        public static Error DuplicateEmail => Error.Conflict(
              code: "User.DuplicateEmail",
              description: "This email is already exist"
          );
    }

}