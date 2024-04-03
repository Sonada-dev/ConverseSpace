using ConverseSpace.Application.Authentication.Services;
using Microsoft.Extensions.DependencyInjection;

namespace ConverseSpace.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(cf => cf.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly));
        
        services.AddScoped<IAuthService,AuthService>();
        
        return services;
    }
}