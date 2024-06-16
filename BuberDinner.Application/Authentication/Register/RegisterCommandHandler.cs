
using BuberDinner.Application.Common.Services;
using BuberDinner.Application.Mapping;
using BuberDinner.Application.Persistence;
using BuberDinner.Domain.Common.Errors;
using BuberDinner.Domain.Entities;

using ErrorOr;

using MediatR;

namespace BuberDinner.Application.Authentication.Register;

public record RegisterCommand(
    string FirstName,
    string LastName,
    string Email,
    string Password
) : IRequest<ErrorOr<AuthenticationResult>>;

public class RegisterCommandHandler :
            IRequestHandler<RegisterCommand, ErrorOr<AuthenticationResult>>
{
    private readonly IUserRepository _userRepository;
    private readonly IJwtTokenGenerator _jwtTokenGenerator;

    public RegisterCommandHandler(
        IUserRepository userRepository,
        IJwtTokenGenerator jwtTokenGenerator)
    {
        _userRepository = userRepository;
        _jwtTokenGenerator = jwtTokenGenerator;
    }

    public async Task<ErrorOr<AuthenticationResult>> Handle(
        RegisterCommand request,
        CancellationToken cancellationToken)
    {
        var existingUser = await _userRepository.GetUserByEmail(request.Email);
        if (existingUser is not null)
        {
            return DomainErrors.User.DuplicateEmail;
        }

        var userId = Guid.NewGuid();
        var user = new User()
        {
            Id = userId,
            FirstName = request.FirstName,
            LastName = request.LastName,
            Email = request.Email,
            Password = request.Password
        };

        // Add user into repository
        await _userRepository.Add(user);

        // Create Token
        var token = _jwtTokenGenerator.GenerateToken(user);

        return MappingExtensions.MapUserEntityToResult(user, token);

    }
}
