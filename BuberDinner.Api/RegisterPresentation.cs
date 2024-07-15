using BuberDinner.Api.Mappings;

using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.OpenApi.Models;

namespace BuberDinner.Api
{
    public static class DependencyInjectionRegister
    {
        public static IServiceCollection AddPresentation(this IServiceCollection services)
        {
            services.AddControllers();
            services.AddSingleton<ProblemDetailsFactory, BuberDinnerProblemDetailsFactory>();
            services.AddMappings();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Your API Title",
                    Version = "v1",
                    Description = "A brief description of your API",
                    Contact = new OpenApiContact
                    {
                        Name = "Your Name",
                        Email = "your.email@example.com"
                    }
                });

                // Configure Swagger to use JWT Bearer authentication
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme. Example: \"Bearer {token}\"",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    Scheme = "bearer",
                    BearerFormat = "JWT"
                });

                // Require JWT token to access API endpoints
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new string[] { }
                    }
                });
            });

            return services;
        }
    }
}

