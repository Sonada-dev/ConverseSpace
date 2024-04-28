using ConverseSpace.Domain.Abstractions.Auth;
using ConverseSpace.Infrastructure.Authentication;
using ConverseSpace.Infrastructure.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ConverseSpace.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(MapperProfile).Assembly);
        
        services.AddScoped<IJwtProvider, JwtProvider>();
        services.AddScoped<IPasswordHasher, PasswordHasher>();
        
        return services;
    }
}