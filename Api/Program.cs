using Api.Common.Mapping;
using Application;
using Application.Authorization;
using Infrastructure;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Extensions.Configuration;
using Serilog.Events;
using Serilog.Sinks.File;
using Serilog;
using Serilog.Formatting.Compact;
using Serilog.Formatting.Json;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((context, configuration) =>
{
    configuration.MinimumLevel.Error();
    configuration.WriteTo.File(path: "./logs/log.txt", rollingInterval: RollingInterval.Day, rollOnFileSizeLimit: true, formatter: new JsonFormatter());

});

// Add services to the container.

builder.Services.AddControllers(
    options => options.Filters.Add(new AuthorizeFilter("EmailVerifiedPolicy"))
);
builder.Services.AddHttpContextAccessor()
    .AddHttpClient();

builder.Services.AddAuthorization(options =>
    {
        options.AddPolicy("EmailVerifiedPolicy", policy => {
            policy.Requirements.Add(new EmailVerifiedRequirement());
        });
    });

{
    builder.Services.AddApplication()
        .AddInfrasturcture(builder.Configuration)
        .AddMapping();
}


var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseExceptionHandler("/error");
app.UseHttpsRedirection();
app.UseAuthorization();

app.MapControllers().RequireAuthorization();

app.Run();
