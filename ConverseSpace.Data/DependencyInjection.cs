using ConverseSpace.Data.Repositories;
using ConverseSpace.Domain.Abstractions.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace ConverseSpace.Data;

public static class DependencyInjection
{
    public static IServiceCollection AddData(this IServiceCollection services)
    {
        services.AddScoped<IUsersRepository, UsersRepository>();
        services.AddScoped<IRolesRepository, RolesRepository>();
        services.AddScoped<ICommunitiesRepository, CommunitiesRepository>();
        services.AddScoped<IJoinRequestsRepository, JoinRequestsRepository>();
        services.AddScoped<IPostsRepository, PostsRepository>();
        services.AddScoped<IFollowsRepository, FollowsRepository>();
        
        return services;
    }
}