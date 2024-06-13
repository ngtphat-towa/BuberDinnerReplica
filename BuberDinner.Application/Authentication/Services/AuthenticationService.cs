using BuberDinner.Application.Common.Services;
using BuberDinner.Application.Persistence;
using BuberDinner.Application.Mapping;
using BuberDinner.Domain.Entities;
using ErrorOr;
using BuberDinner.Domain.Common.Errors;


namespace BuberDinner.Application;

public interface IAuthenticationService
{
    ErrorOr<AuthenticationResult> Login(string email, string password);
    ErrorOr<AuthenticationResult> Register(string firstName, string lastName, string email, string password);
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

    public ErrorOr<AuthenticationResult> Login(string email, string password)
    {
        // validate existing of the user
        var existingUser = _userRepository.GetUserByEmail(email);
        if (existingUser is null || !existingUser.Password.Equals(password))
        {
            return Errors.Auth.InvalidCredential;
        }
        // Create Token
        var token = _jwtTokenService.GenerateToken(existingUser);

        return MappingExtensions.MapUserEntityToResult(existingUser, token);
    }

    public ErrorOr<AuthenticationResult> Register(string firstName, string lastName, string email, string password)
    {
        // validate existing of the user
        var existingUser = _userRepository.GetUserByEmail(email);
        if (existingUser is not null)
        {
            return Errors.User.DuplicateEmail;
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