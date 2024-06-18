using BuberDinner.Application;
using BuberDinner.Contracts.Authentication;

using Mapster;

namespace BuberDinner.Api.Mappings;

public class AuthMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        // Manual mapping
        // The rest will be automatically handled 
        config.NewConfig<AuthenticationResult, AuthenticationResponse>()
              .Map(dest => dest.Token, src => src.Token)
              .Map(dest => dest, src => src.User)
              .Map(dest => dest.Id, src => src.User.Id.Value);
    }
}