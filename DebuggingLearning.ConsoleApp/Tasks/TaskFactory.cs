using System;
using System.Collections.Generic;
using DebuggingLearning.ConsoleApp.Tasks.Interfaces;
using Microsoft.Extensions.Logging;

namespace DebuggingLearning.ConsoleApp.Tasks;

public class TaskFactory : ITaskFactory
{
    private readonly ILogger<TaskFactory> _logger;
    private readonly IEnumerable<ITask> _tasks;

    public TaskFactory(ILogger<TaskFactory> logger, IEnumerable<ITask> tasks)
    {
        _logger = logger;
        _tasks = tasks;
    }

    public ITask? Build(string name)
    {
        foreach (var task in _tasks)
        {
            if (string.Equals(task.GetType().Name, name, StringComparison.InvariantCultureIgnoreCase))
            {
                return task;
            }
        }

        _logger.LogWarning($"{nameof(Build)} - the task '{name}' is not supported");
        return null;
    }
}
