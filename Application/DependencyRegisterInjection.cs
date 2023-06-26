using Application.Authorization;
using Application.Authorization.Handlers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;

namespace Application;

public static class DependencyRegisterInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(DependencyRegisterInjection).Assembly));

        //    services.AddScoped(typeof(IPipelineBehavior<,>));

        //   services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

        services.AddScoped<IAuthorizationHandler, EmailVerifiedHandler>();
        services.AddScoped<IAuthorizationHandler, MovieListOwnerAuthorizationHandler>();

        return services;
    }
}
