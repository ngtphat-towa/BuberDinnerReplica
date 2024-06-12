using BuberDinner.Application;
using BuberDinner.Application.Common.Errors;
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
        var authResult = _authenticationService.Login(loginRequest.Email, loginRequest.Password);

        var response = new AuthenticationResponse(
            authResult.User.Id,
            authResult.User.FirstName,
            authResult.User.LastName,
            authResult.User.Email,
            authResult.Token);

        return Ok(response);
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

        if (registerResult.IsSuccess)
        {
            var authResult = registerResult.Value;
            var response = new AuthenticationResponse(
                               authResult.User.Id,
                               authResult.User.FirstName,
                               authResult.User.LastName,
                               authResult.User.Email,
                               authResult.Token);
            return Ok(response);
        }
        if (registerResult.Errors.First() is DuplicatedEmailError error)
        {
            return Problem(
                statusCode: StatusCodes.Status409Conflict,
                title: error.Message);
        }
        return Problem();
    }
}
