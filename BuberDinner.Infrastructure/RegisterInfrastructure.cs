using BuberDinner.Application.Common.Services;
using BuberDinner.Application.Persistence;
using BuberDinner.Infrastructure.Authentication;
using BuberDinner.Infrastructure.Common.Services;
using BuberDinner.Infrastructure.Persistence;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BuberDinner.Infrastructure;

public static class RegisterInfrastructure
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.Configure<JwtSettings>(configuration.GetSection(JwtSettings.SectionName));

        services.AddAuthentication();
        services.AddCommonService();
        services.AddPersistence();


        return services;
    }
    public static IServiceCollection AddCommonService(this IServiceCollection services)
    {

        services.AddSingleton<IJwtTokenGenerator, JwtTokenGenerator>();
        services.AddSingleton<IDateTimeProvider, DateTimeProvider>();
        return services;
    }
    public static IServiceCollection AddPersistence(this IServiceCollection services)
    {
        services.AddSingleton<IUserRepository, UserRepository>();
        return services;
    }
}