using BuberDinner.Application;
using BuberDinner.Application.Authentication.Login;
using BuberDinner.Application.Authentication.Register;
using BuberDinner.Contracts.Authentication;

using MediatR;

using Microsoft.AspNetCore.Mvc;

namespace BuberDinner.Api;

public class AuthenticationController : ApiControllerBase
{
    private readonly ISender _mediator;

    public AuthenticationController(ISender mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginRequest loginRequest)
    {
        var query = new LoginQuery(
            loginRequest.Email,
            loginRequest.Password
        );

        var authResult = await _mediator.Send(query);

        return authResult.Match(
             result => Ok(MapResultToResponse(result)),
             errors => Problem(errors)
         );
    }
    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterRequest registerRequest)
    {
        var command = new RegisterCommand(
            registerRequest.FirstName,
            registerRequest.LastName,
            registerRequest.Email,
            registerRequest.Password);

        var authResult = await _mediator.Send(command);

        return authResult.Match(
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
