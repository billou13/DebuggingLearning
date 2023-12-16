using System;

namespace DebuggingLearning.WebApi.Models;

public class TaskModel
{
    public Guid Id { get; set; }
    public required TaskTypeModel TaskType { get; set; }
    public required TaskStatusModel Status { get; set; }
}
