using System.Threading;
using System.Threading.Tasks;
using DebuggingLearning.Tasks.Interfaces;
using Microsoft.Extensions.Logging;

namespace DebuggingLearning.ConsoleApp.Bootstrap;

public class Application
{
    private readonly ILogger<Application> _logger;
    private readonly ITaskFactory _taskFactory;

    public Application(ILogger<Application> logger, ITaskFactory taskFactory)
    {
        _logger = logger;
        _taskFactory = taskFactory;
    }

    public async Task RunAsync(string[] args)
    {
        string name = args[0];
        _logger.LogDebug($"{nameof(RunAsync)} - loading task '{name}'...");

        var task = _taskFactory.Build(name);
        if (task is null)
        {
            return;
        }

        var cancelToken = new CancellationTokenSource();
        await task.RunAsync(cancelToken.Token);
    }
}
