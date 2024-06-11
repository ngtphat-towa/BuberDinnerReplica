using BuberDinner.Application.Authentication;

namespace BuberDinner.Application;

public interface IAuthenticationService
{
    AuthenticationResult Login(string email, string password);
    AuthenticationResult Register(string firstName, string lastName, string email, string password);
}
public class AuthenticationService : IAuthenticationService
{
    private readonly IJwtTokenService _jwtTokenService;

    public AuthenticationService(IJwtTokenService jwtTokenService)
    {
        _jwtTokenService = jwtTokenService;
    }

    public AuthenticationResult Login(string email, string password)
    {

        // TODO: validate existing of the user

        // TODO: Create user 
        var userId = Guid.NewGuid();
        // Create Token
        var token = _jwtTokenService.GenerateToken(
            userId,
            "firstName",
            "lastName");

        return new AuthenticationResult(Guid.NewGuid(), "firstName", "lastName", email, token);
    }

    public AuthenticationResult Register(string firstName, string lastName, string email, string password)
    {
        // TODO: validate existing of the user

        // TODO: Create user 
        var userId = Guid.NewGuid();
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