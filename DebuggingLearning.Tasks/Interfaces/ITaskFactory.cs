namespace DebuggingLearning.Tasks.Interfaces;

public interface ITaskFactory
{
    ITask? Build(string name);
}
