using BuberDinner.Api.Filters;
using BuberDinner.Application;
using BuberDinner.Infrastructure;

var builder = WebApplication.CreateBuilder(args);
{
    builder.Services.AddApplication();
    builder.Services.AddInfrastructure(builder.Configuration);
    // builder.Services.AddControllers();

    builder.Services.AddControllers(options => options.Filters.Add<ErrorHandlingFilterAttribute>());
}

var app = builder.Build();
{
    app.UseHttpsRedirection();
    app.MapControllers();
    app.Run();
}