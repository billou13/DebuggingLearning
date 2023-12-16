using DebuggingLearning.Tasks.Configuration.Abstractions;

namespace DebuggingLearning.Tasks.Configuration;

public class MemoryLeakTaskConfig : RecurringTaskConfig
{
    public required int GrowthSize { get; set; }
}
