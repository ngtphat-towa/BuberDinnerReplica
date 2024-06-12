

using BuberDinner.Application.Common.Services;
using BuberDinner.Application.Persistence;
using BuberDinner.Application.Mapping;
using BuberDinner.Domain.Entities;
using FluentResults;
using BuberDinner.Application.Common.Errors;

namespace BuberDinner.Application;

public interface IAuthenticationService
{
    AuthenticationResult Login(string email, string password);
    Result<AuthenticationResult> Register(string firstName, string lastName, string email, string password);
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
        var existingUser = _userRepository.GetUserByEmail(email);
        if (existingUser is null || !existingUser.Password.Equals(password))
        {
            throw new Exception("Invalid user or password");
        }

        // Create Token
        var token = _jwtTokenService.GenerateToken(existingUser);

        return MappingExtensions.MapUserEntityToResult(existingUser, token);
    }

    public Result<AuthenticationResult> Register(string firstName, string lastName, string email, string password)
    {
        // validate existing of the user
        var existingUser = _userRepository.GetUserByEmail(email);
        if (existingUser is not null)
        {
            return Result.Fail(new DuplicatedEmailError());
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
        var token = _jwtTokenService.GenerateToken(user);

        return MappingExtensions.MapUserEntityToResult(user, token);

    }


}