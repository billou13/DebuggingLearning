using System;

namespace DebuggingLearning.Tasks.Exceptions;

public class TaskException : Exception
{
    public TaskException(string message)
        : base(message)
    { }
}
