using Ardalis.Result;
using Ardalis.Result.AspNetCore;

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
    public async Task<ActionResult<AuthenticationResponse>> Register(RegisterRequest registerRequest)
    {
        await Task.CompletedTask;
        return _authenticationService.Register(
            registerRequest.FirstName,
            registerRequest.LastName,
            registerRequest.Email,
            registerRequest.Password).Map(
            x => new AuthenticationResponse(
            x.User.Id,
            x.User.FirstName,
            x.User.LastName,
            x.User.Email,
            x.Token)
        ).ToActionResult(this);
    }
}
