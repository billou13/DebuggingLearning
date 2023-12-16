using System;
using System.Collections.Generic;
using DebuggingLearning.Tasks.Configuration;
using DebuggingLearning.Tasks.Abstractions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace DebuggingLearning.Tasks;

public class MemoryLeakTask : RecurringTask<MemoryLeakTask, MemoryLeakTaskConfig>
{
    private readonly List<int> _items = new List<int>();
    private readonly Random _rand = new Random();

    public MemoryLeakTask(IConfiguration configuration, ILogger<MemoryLeakTask> logger)
        : base(configuration, logger)
    {
    }

    protected override void Execute()
    {
        _logger.LogInformation($"adding {Configuration.GrowthSize:n0} items...");
        for (int i = 0; i < Configuration.GrowthSize; i++)
        {
            _items.Add(_rand.Next());
        }
    }
}
