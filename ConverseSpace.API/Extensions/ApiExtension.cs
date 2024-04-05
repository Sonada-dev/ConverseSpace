using System.Text;
using ConverseSpace.Infrastructure.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace ConverseSpace.API.Extensions;

public static class ApiExtension
{
    public static void AddApiAuthentication(this IServiceCollection services, IConfiguration configuration)
    {
        var jwtOptions = configuration.GetSection(nameof(JwtOptions)).Get<JwtOptions>();
        
        services
            .AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultSignInScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
            {
                options.RequireHttpsMetadata = true;
                options.SaveToken = true;
                
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(jwtOptions!.SecretKey))
                };

                options.Events = new JwtBearerEvents
                {
                    OnMessageReceived = context =>
                    {
                        context.Token = context.Request.Cookies["746F6B656E"];
                        
                        return Task.CompletedTask;
                    }
                };
            });

        services.AddAuthorizationBuilder()
            .AddPolicy("UserPolicy", policy =>
            {
                //policy.RequireClaim(ClaimTypes.Role, "3");
                policy.RequireRole("Пользователь");
            })
            .AddPolicy("ModerPolicy", policy =>
            {
                //policy.RequireClaim(ClaimTypes.Role, "2");
                policy.RequireRole("Модератор", "Пользователь");
            })
            .AddPolicy("AdminPolicy", policy =>
            {
                //policy.RequireClaim(ClaimTypes.Role,"1");
                policy.RequireRole("Администратор", "Модератор", "Пользователь");
            });
    }
}