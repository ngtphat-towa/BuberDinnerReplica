using BuberDinner.Application.Features.Authentication.Login;
using BuberDinner.Application.Features.Authentication.Models;
using BuberDinner.Application.Features.Authentication.Register;
using BuberDinner.Contracts.Authentication;

using MapsterMapper;

using MediatR;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BuberDinner.Api
{
    /// <summary>
    /// Controller for user authentication operations.
    /// </summary>
    [AllowAnonymous]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public class AuthController : ApiControllerBase
    {
        private readonly ISender _mediator;
        private readonly IMapper _mapper;

        public AuthController(ISender mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        /// <summary>
        /// Endpoint for user login.
        /// </summary>
        /// <param name="request">Login request containing user credentials.</param>
        /// <returns>Returns authentication response upon successful login.</returns>
        [HttpPost("login")]
        [ProducesResponseType(typeof(AuthenticationResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> Login(LoginRequest request)
        {
            var query = _mapper.Map<LoginQuery>(request);

            var authResult = await _mediator.Send(query);

            return authResult.Match(
                 result => Ok(MapResultToResponse(result)),
                 errors => Problem(errors)
             );
        }

        /// <summary>
        /// Endpoint for user registration.
        /// </summary>
        /// <param name="request">Registration request containing user details.</param>
        /// <returns>Returns authentication response upon successful registration.</returns>
        [HttpPost("register")]
        [ProducesResponseType(typeof(AuthenticationResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status409Conflict)]
        public async Task<IActionResult> Register(RegisterRequest request)
        {
            var command = _mapper.Map<RegisterCommand>(request);

            var authResult = await _mediator.Send(command);

            return authResult.Match(
                result => Ok(MapResultToResponse(result)),
                errors => Problem(errors)
            );
        }

        private AuthenticationResponse MapResultToResponse(AuthenticationResult authResult)
        {
            return _mapper.Map<AuthenticationResponse>(authResult);
        }
    }
}
