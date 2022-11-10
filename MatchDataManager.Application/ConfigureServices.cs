using System.Reflection;
using FluentValidation;
using MatchDataManager.Application.Common.Behavior;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace MatchDataManager.Application;

public static class ConfigureServices
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddMediatR(Assembly.GetExecutingAssembly());
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

        services.AddScoped(
            typeof(IPipelineBehavior<,>),
            typeof(ValidationBehavior<,>));

        return services;
    }
}