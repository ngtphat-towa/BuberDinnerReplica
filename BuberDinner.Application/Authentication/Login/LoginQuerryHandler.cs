
using BuberDinner.Application.Common.Services;
using BuberDinner.Application.Mapping;
using BuberDinner.Application.Persistence;
using BuberDinner.Domain.Common.Errors;

using ErrorOr;

using MediatR;

namespace BuberDinner.Application.Authentication.Login;


public class LoginQueryHandler :
    IRequestHandler<LoginQuery, ErrorOr<AuthenticationResult>>
{
    private readonly IJwtTokenGenerator _jwtTokenGenerator;
    private readonly IUserRepository _userRepository;

    public LoginQueryHandler(IJwtTokenGenerator jwtTokenGenerator, IUserRepository userRepository)
    {
        _jwtTokenGenerator = jwtTokenGenerator;
        _userRepository = userRepository;
    }

    public async Task<ErrorOr<AuthenticationResult>> Handle(LoginQuery request, CancellationToken cancellationToken)
    {
        var existingUser = await _userRepository.GetUserByEmail(request.Email);
        if (existingUser is null)
        {
            return DomainErrors.Auth.InvalidCredentials;
        }
        if (existingUser.Password != request.Password)
        {
            return DomainErrors.Auth.InvalidCredentials;
        }
        // Create Token
        var token = _jwtTokenGenerator.GenerateToken(existingUser);

        return MappingExtensions.MapUserEntityToResult(existingUser, token);
    }
}