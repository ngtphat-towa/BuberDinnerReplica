using BuberDinner.Domain.User;

namespace BuberDinner.Application.Features.Authentication.Models;

public record AuthenticationResult(
    User User,
    string Token);