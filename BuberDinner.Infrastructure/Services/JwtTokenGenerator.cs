using Microsoft.IdentityModel.JsonWebTokens;

using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using BuberDinner.Application.Common.Services;
using BuberDinner.Domain.User;
using BuberDinner.Infrastructure.Authentication;

namespace BuberDinner.Infrastructure.Services
{
    public class JwtTokenGenerator : IJwtTokenGenerator
    {
        private readonly IDateTimeProvider _dateTimeProvider;
        private readonly JwtSettings _jwtSettings;

        public JwtTokenGenerator(IDateTimeProvider dateTimeProvider, IOptions<JwtSettings> jwtOptions)
        {
            _dateTimeProvider = dateTimeProvider;
            _jwtSettings = jwtOptions.Value;
        }

        public string GenerateToken(User user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Secret));

            var claims = new Dictionary<string, object>
            {
                [JwtRegisteredClaimNames.Sub] = user.Id.Value.ToString(),
                [JwtRegisteredClaimNames.GivenName] = user.FirstName,
                [JwtRegisteredClaimNames.FamilyName] = user.LastName,
                [JwtRegisteredClaimNames.Jti] = Guid.NewGuid().ToString(),
            };

            var descriptor = new SecurityTokenDescriptor
            {
                Issuer = _jwtSettings.Issuer,
                Audience = _jwtSettings.Audience,
                Claims = claims,
                Expires = _dateTimeProvider.UtcNow.AddMinutes(_jwtSettings.ExpiryMinutes),
                SigningCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256),
            };

            var handler = new JsonWebTokenHandler
            {
                SetDefaultTimesOnTokenCreation = false
            };
            var tokenString = handler.CreateToken(descriptor);

            return tokenString;
        }
    }
}