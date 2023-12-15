using System.Threading.Tasks;
using DebuggingLearning.ConsoleApp.Configuration.Abstractions;
using DebuggingLearning.ConsoleApp.Tasks.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace DebuggingLearning.ConsoleApp.Tasks.Abstractions;

public abstract class SingleTask<TTask, TConfig> : ITask
    where TTask : ITask
    where TConfig : SingleTaskConfig
{
    private readonly IConfiguration _configuration;
    protected readonly ILogger<TTask> _logger;

    public SingleTask(IConfiguration configuration, ILogger<TTask> logger)
    {
        _configuration = configuration;
        _logger = logger;
    }

    protected TConfig Configuration
    {
        get { return _configuration.GetSection(GetType().Name).Get<TConfig>()!; }
    }

    protected abstract void Run();

    public async Task RunAsync()
    {
        Run();
        await Task.CompletedTask;
    }
}
