namespace DebuggingLearning.ConsoleApp.Tasks.Interfaces;

public interface ITaskFactory
{
    ITask? Build(string name);
}
