using DebuggingLearning.ConsoleApp.Configuration.Abstractions;

namespace DebuggingLearning.ConsoleApp.Configuration;

public class MemoryLeakTaskConfig : RecurringTaskConfig
{
    public required int GrowthSize { get; set; }
}
