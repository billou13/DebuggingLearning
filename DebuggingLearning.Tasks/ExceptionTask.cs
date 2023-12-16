using DebuggingLearning.Tasks.Configuration;
using DebuggingLearning.Tasks.Abstractions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using DebuggingLearning.Tasks.Exceptions;
using System;

namespace DebuggingLearning.Tasks;

public class ExceptionTask : SingleTask<ExceptionTask, ExceptionTaskConfig>
{
    public ExceptionTask(IConfiguration configuration, ILogger<ExceptionTask> logger)
        : base(configuration, logger)
    {
    }

    protected override void Execute()
    {
        throw new TaskException($"An error occured at '{DateTime.UtcNow}'");
    }
}
