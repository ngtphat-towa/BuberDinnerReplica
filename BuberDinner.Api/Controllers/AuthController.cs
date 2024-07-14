using BuberDinner.Application;
using BuberDinner.Application.Authentication.Login;
using BuberDinner.Application.Authentication.Register;
using BuberDinner.Contracts.Authentication;

using MapsterMapper;

using MediatR;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BuberDinner.Api;

[AllowAnonymous]
public class AuthController : ApiControllerBase
{
    private readonly ISender _mediator;
    private readonly IMapper _mapper;

    public AuthController(ISender mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginRequest request)
    {
        var query = _mapper.Map<LoginQuery>(request);

        var authResult = await _mediator.Send(query);

        return authResult.Match(
             result => Ok(MapResultToResponse(result)),
             errors => Problem(errors)
         );
    }
    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterRequest request)
    {
        var command = _mapper.Map<RegisterCommand>(request);

        var authResult = await _mediator.Send(command);

        return authResult.Match(
            result => Ok(MapResultToResponse(result)),
            errors => Problem(errors)
        );
    }
    private AuthenticationResponse MapResultToResponse(
        AuthenticationResult authResult) => _mapper.Map<AuthenticationResponse>(authResult);
}
