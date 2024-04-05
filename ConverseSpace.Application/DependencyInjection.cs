using ConverseSpace.Application.Authentication.Services;
using ConverseSpace.Application.Users.Services;
using ConverseSpace.Domain.Abstractions.Services;
using Microsoft.Extensions.DependencyInjection;

namespace ConverseSpace.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(cf => cf.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly));
        
        services.AddScoped<IAuthService,AuthService>();
        services.AddScoped<IUsersService,UsersService>();
        
        return services;
    }
}