using Application.Interfaces;
using Aspire.StackExchange.Redis;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.UnitOfWork;
using FluentValidation;
using Infrastructure.Context;
using Infrastructure.Repositories;
using Infrastructure.Services.Cache;
using Infrastructure.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using Npgsql.EntityFrameworkCore.PostgreSQL;
using StackExchange.Redis;

namespace API.Extensions;

public static class ServicesRegistration
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration, IConnectionMultiplexer? redis = null)
    {
        services.AddDbContext<DataContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));

        services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        
        if (redis != null)
        {
            services.AddSingleton(redis);
        }
        services.AddScoped<ICachingService, RedisCachingService>();

        return services;
    }

    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        var applicationAssembly = typeof(Application.Behaviors.ValidationBehavior<,>).Assembly;
        
        services.AddValidatorsFromAssembly(applicationAssembly);

        return services;
    }
}

public static class AspireRedisExtensions
{
    public static IHostApplicationBuilder AddRedisInfrastructure(this IHostApplicationBuilder builder)
    {
        builder.AddRedisClient("redis");
        return builder;
    }
}
