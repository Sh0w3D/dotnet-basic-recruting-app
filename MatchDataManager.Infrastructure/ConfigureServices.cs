using MatchDataManager.Application.Common.Interfaces;
using MatchDataManager.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace MatchDataManager.Infrastructure;

public static class ConfigureServices
{
    public static IServiceCollection AddInfrastructureServices(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection")
            .Replace("{dbPath}",
                string.Concat(configuration["dbPath"], Path.DirectorySeparatorChar));

        services.AddDbContext<ApplicationDbContext>(
            options => options.UseSqlite(connectionString,
                builder => builder
                    .MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));

        services.AddScoped<IApplicationDbContext>(
            provider => provider.GetRequiredService<ApplicationDbContext>());

        services.AddScoped<ApplicationDbContextInitializer>();

        return services;
    }
}