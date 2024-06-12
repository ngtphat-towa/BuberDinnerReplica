using BuberDinner.Domain.Entities;

namespace BuberDinner.Application;

public record AuthenticationResult(
    User User,
    string Token);