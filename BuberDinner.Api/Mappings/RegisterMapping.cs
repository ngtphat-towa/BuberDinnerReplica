using System.Reflection;

using BuberDinner.Application.Common.Wrappers;
using BuberDinner.Domain.Wrapper;

using Mapster;

using MapsterMapper;

namespace BuberDinner.Api.Mappings;

public static class RegisterMapping
{
    public static IServiceCollection AddMappings(this IServiceCollection services)
    {
        var config = TypeAdapterConfig.GlobalSettings;
        config.Scan(Assembly.GetExecutingAssembly());

        services.AddSingleton(config);
        services.AddScoped<IMapper, ServiceMapper>();
        return services;
    }
}