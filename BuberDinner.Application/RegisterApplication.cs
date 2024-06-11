using Microsoft.Extensions.DependencyInjection;

namespace BuberDinner.Application;

public static class RegisterApplication
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IAuthenticationService, AuthenticationService>();
        return services;
    }
}