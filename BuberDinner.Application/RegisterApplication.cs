using MediatR;

using Microsoft.Extensions.DependencyInjection;

namespace BuberDinner.Application;

public static class RegisterApplication
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(RegisterApplication).Assembly));
        return services;
    }
}