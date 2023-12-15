using DebuggingLearning.ConsoleApp.Configuration;
using DebuggingLearning.ConsoleApp.Tasks.Abstractions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace DebuggingLearning.ConsoleApp.Tasks;

public class ExceptionTask : SingleTask<ExceptionTask, ExceptionTaskConfig>
{
    public ExceptionTask(IConfiguration configuration, ILogger<ExceptionTask> logger)
        : base(configuration, logger)
    {
    }

    protected override void Run()
    {
        throw new System.NotImplementedException();
    }
}
