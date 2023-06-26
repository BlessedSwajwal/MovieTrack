using Application.Common.Interfaces;
using Application.Common.Interfaces.Authentication;
using Application.Common.Interfaces.Persistence;
using Application.Common.Interfaces.Service;
using Infrastructure.Authentication;
using Infrastructure.Email;
using Infrastructure.Movie;
using Infrastructure.Persistence;
using Infrastructure.Persistence.Persistence;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Infrastructure;

public static class DependencyInjectionRegister
{
    public static IServiceCollection AddInfrasturcture(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<TrackItDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("TraktItDb"))
        );

        //TMDB ID
        string tmdbAPI = configuration.GetValue<string>("TMDB");
        services.AddSingleton(tmdbAPI);

        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IMovieListRepository, MovieListRepository>();
        services.AddScoped<IStreamedListRepository, StreamedListRepository>();

        services.AddAuthentication(configuration)
            .AddScoped<IMovieRepository, MovieRepository>();


        var emailConfig = new EmailConfiguration();
        configuration.GetSection(EmailConfiguration.Section).Bind(emailConfig);
        services.AddSingleton(Options.Create(emailConfig));
        services.AddScoped<IEmailService, VerificationEmailService>();

        return services;
    }

    private static IServiceCollection AddAuthentication(this IServiceCollection services, IConfiguration configuration)
    {
        var jwtSettings = new JwtSettings();
        configuration.GetSection(JwtSettings.SectionName).Bind(jwtSettings);
        services.AddSingleton(Options.Create(jwtSettings));

        services.AddSingleton<IJwtTokenGenerator, JwtTokenGenerator>();
        services.AddSingleton<IGenerateToken, GenerateToken>();

        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(
                options => options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateAudience= true,
                    ValidateIssuer= true,
                    ValidateLifetime= true,
                    ValidateIssuerSigningKey= true,
                    ValidAudience = jwtSettings.Audience,
                    ValidIssuer = jwtSettings.Issuer,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Secret))
                }
            );

        return services;
    }
}
