using DebuggingLearning.WebApi.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace DebuggingLearning.WebApi.Services.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services
            .AddSingleton<ICommandService, CommandService>()
            .AddSingleton<ITaskService, TaskService>();

        return services;
    }
}
