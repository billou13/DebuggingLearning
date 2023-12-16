using System;
using System.Threading;
using System.Threading.Tasks;
using DebuggingLearning.Tasks.Configuration.Abstractions;
using DebuggingLearning.Tasks.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace DebuggingLearning.Tasks.Abstractions;

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

    protected abstract void Execute();

    public async Task RunAsync(CancellationToken token)
    {
        _ = Task.Run(() => BackgroundRun(token));
        await Task.CompletedTask;
    }

    private bool TryExecute()
    {
        try
        {
            Execute();
            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"{nameof(TryExecute)} - an error occured while executing task.");
            return false;
        }
    }

    private void BackgroundRun(CancellationToken token)
    {
        if (token.IsCancellationRequested)
        {
            _logger.LogInformation($"{nameof(BackgroundRun)} - cancellation has been requested...");
            return;
        }

        TryExecute();
    }
}
