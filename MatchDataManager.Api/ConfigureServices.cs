using MatchDataManager.Api.Common.Exceptions;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace MatchDataManager.Api;

public static class ConfigureServices
{
    public static IServiceCollection AddApiServices(this IServiceCollection services)
    {
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();

        services.AddSingleton<ProblemDetailsFactory, MatchDataManagerProblemDetailsFactory>();

        services.AddControllers();
        return services;
    }
}