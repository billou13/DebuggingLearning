namespace DebuggingLearning.ConsoleApp.Configuration.Abstractions;

public abstract class RecurringTaskConfig
{
    public required int DelayMs { get; set; }
}
