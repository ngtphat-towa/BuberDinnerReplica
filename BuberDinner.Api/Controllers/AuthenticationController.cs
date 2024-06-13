using BuberDinner.Application;
using BuberDinner.Contracts.Authentication;

using Microsoft.AspNetCore.Mvc;

namespace BuberDinner.Api;

public class AuthenticationController : ApiControllerBase
{
    private readonly IAuthenticationService _authenticationService;

    public AuthenticationController(IAuthenticationService authenticationService)
    {
        _authenticationService = authenticationService;
    }


    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginRequest loginRequest)
    {
        await Task.CompletedTask;
        var loginResult = _authenticationService.Login(loginRequest.Email, loginRequest.Password);

        return loginResult.Match(
             result => Ok(MapResultToResponse(result)),
             errors => Problem(errors)
         );
    }
    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterRequest registerRequest)
    {
        await Task.CompletedTask;
        var registerResult = _authenticationService.Register(
            registerRequest.FirstName,
            registerRequest.LastName,
            registerRequest.Email,
            registerRequest.Password);

        return registerResult.Match(
            result => Ok(MapResultToResponse(result)),
            errors => Problem(errors)
        );
    }
    private static AuthenticationResponse MapResultToResponse(
        AuthenticationResult authResult) => new(
            authResult.User.Id,
            authResult.User.FirstName,
            authResult.User.LastName,
            authResult.User.Email,
            authResult.Token);
}
