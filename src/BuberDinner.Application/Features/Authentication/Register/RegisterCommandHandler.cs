using BuberDinner.Application.Common.Services;
using BuberDinner.Application.Features.Authentication.Models;
using BuberDinner.Application.Interfaces.Repositories;
using BuberDinner.Application.Mapping;
using BuberDinner.Domain.Common.Errors;
using BuberDinner.Domain.User;

using ErrorOr;

using MediatR;

namespace BuberDinner.Application.Features.Authentication.Register;


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
        RegisterCommand command,
        CancellationToken cancellationToken)
    {
        var existingUser = await _userRepository.GetUserByEmail(command.Email);
        if (existingUser is not null)
        {
            return DomainErrors.User.DuplicateEmail;
        }

        var userId = Guid.NewGuid();
        var user = User.Create
        (
            command.FirstName,
            command.LastName,
            command.Email,
            command.Password
        );

        // Add user into repository
        await _userRepository.Add(user);

        // Create Token
        var token = _jwtTokenGenerator.GenerateToken(user);

        return MappingExtensions.MapUserEntityToResult(user, token);

    }
}
