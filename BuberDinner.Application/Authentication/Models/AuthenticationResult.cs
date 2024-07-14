using BuberDinner.Domain.User;

namespace BuberDinner.Application;

public record AuthenticationResult(
    User User,
    string Token);