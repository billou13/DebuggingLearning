using DebuggingLearning.Tasks.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace DebuggingLearning.Tasks.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddTasks(this IServiceCollection services)
    {
        services
            .AddSingleton<ITask, ExceptionTask>()
            .AddSingleton<ITask, MemoryLeakTask>();

        services.AddSingleton<ITaskFactory, TaskFactory>();

        return services;
    }
}
