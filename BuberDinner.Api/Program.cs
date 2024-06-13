using System.Net;

using Ardalis.Result;
using Ardalis.Result.AspNetCore;

using BuberDinner.Application;
using BuberDinner.Infrastructure;

using Microsoft.AspNetCore.Mvc.Infrastructure;

var builder = WebApplication.CreateBuilder(args);
{
    builder.Services.AddApplication();
    builder.Services.AddInfrastructure(builder.Configuration);
    builder.Services.AddControllers(mvcOptions => mvcOptions
        .AddResultConvention(resultStatusMap => resultStatusMap
            .AddDefaultMap()
            .For(ResultStatus.Ok, HttpStatusCode.OK, resultStatusOptions => resultStatusOptions
                .For("POST", HttpStatusCode.Created)
                .For("DELETE", HttpStatusCode.NoContent))
            .Remove(ResultStatus.Forbidden)
            .Remove(ResultStatus.Unauthorized)
        ));



    builder.Services.AddSingleton<ProblemDetailsFactory, BuberDinnerProblemDetailsFactory>();
}

var app = builder.Build();
{
    app.UseExceptionHandler("/error");
    app.UseHttpsRedirection();
    app.MapControllers();
    app.Run();
}