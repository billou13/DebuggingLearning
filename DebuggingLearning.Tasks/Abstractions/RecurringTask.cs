using System.Threading.Tasks;
using DebuggingLearning.Tasks.Configuration.Abstractions;
using DebuggingLearning.Tasks.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace DebuggingLearning.Tasks.Abstractions;

public abstract class RecurringTask<TTask, TConfig> : ITask
    where TTask : ITask
    where TConfig : RecurringTaskConfig
{
    private readonly IConfiguration _configuration;
    protected readonly ILogger<TTask> _logger;

    public RecurringTask(IConfiguration configuration, ILogger<TTask> logger)
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
        while (true)
        {
            Run();
            await Task.Delay(Configuration.DelayMs);
        }
    }
}
