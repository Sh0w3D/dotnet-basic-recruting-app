using MatchDataManager.Application.Common.Interfaces;
using MatchDataManager.Application.Common.Interfaces.Repositories.Command;
using MatchDataManager.Application.Common.Interfaces.Repositories.Query;
using MatchDataManager.Infrastructure.Persistence;
using MatchDataManager.Infrastructure.Repositories.Command;
using MatchDataManager.Infrastructure.Repositories.Command.Query;
using MatchDataManager.Infrastructure.Repositories.Queries;
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
        #region DbContext
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
        
        #endregion

        #region QueryRepository

        services.AddScoped<ILocationQueryRepository, LocationQueryRepository>();
        services.AddScoped<ITeamQueryRepository, TeamQueryRepository>();
        
        #endregion

        #region CommandRepository

        services.AddScoped<ILocationCommandRepository, LocationCommandRepository>();
        services.AddScoped<ITeamCommandRepository, TeamCommandRepository>();
        
        #endregion

        return services;
    }
}