

using BuberDinner.Application.Common.Services;
using BuberDinner.Application.Persistence;
using BuberDinner.Domain.Entities;

namespace BuberDinner.Application;

public interface IAuthenticationService
{
    AuthenticationResult Login(string email, string password);
    AuthenticationResult Register(string firstName, string lastName, string email, string password);
}
public class AuthenticationService : IAuthenticationService
{
    private readonly IJwtTokenGenerator _jwtTokenService;
    private readonly IUserRepository _userRepository;

    public AuthenticationService(IJwtTokenGenerator jwtTokenService, IUserRepository userRepository)
    {
        _jwtTokenService = jwtTokenService;
        _userRepository = userRepository;
    }

    public AuthenticationResult Login(string email, string password)
    {
        // validate existing of the user
        var user = _userRepository.GetUserByEmail(email);
        if (user is null || !user.Password.Equals(password))
        {
            throw new Exception("Invalid user or password");
        }

        // Create Token
        var token = _jwtTokenService.GenerateToken(
            user.Id,
            user.FirstName,
            user.LastName);

        return new AuthenticationResult(
            user.Id,
            user.FirstName,
            user.LastName,
            email,
            token);
    }

    public AuthenticationResult Register(string firstName, string lastName, string email, string password)
    {
        // validate existing of the user
        var existingUser = _userRepository.GetUserByEmail(email);
        if (existingUser is not null)
        {
            throw new Exception("The give email already existed");
        }

        var userId = Guid.NewGuid();
        var user = new User()
        {
            Id = userId,
            FirstName = firstName,
            LastName = lastName,
            Email = email,
            Password = password
        };

        // Add user into repository
        _userRepository.Add(user);

        // Create Token
        var token = _jwtTokenService.GenerateToken(
            userId,
            firstName,
            lastName);

        return new AuthenticationResult(
            userId,
            firstName,
            lastName,
            email,
            token);
    }
}