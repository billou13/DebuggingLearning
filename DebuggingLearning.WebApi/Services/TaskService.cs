using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using DebuggingLearning.Tasks.Interfaces;
using DebuggingLearning.WebApi.Models;
using DebuggingLearning.WebApi.Services.Interfaces;
using Microsoft.Extensions.Logging;

namespace DebuggingLearning.WebApi.Services;

public class TaskService : ITaskService
{
    private readonly ILogger<TaskService> _logger;
    private readonly ITaskFactory _taskFactory;

    private record StoredTask(TaskModel model, CancellationTokenSource cancelToken);
    private readonly ConcurrentDictionary<Guid, StoredTask> _storedTasks;

    public TaskService(ILogger<TaskService> logger, ITaskFactory taskFactory)
    {
        _logger = logger;
        _taskFactory = taskFactory;

        _storedTasks = new ConcurrentDictionary<Guid, StoredTask>();
    }

    public async Task<IEnumerable<TaskModel>> GetAllAsync()
    {
        await Task.CompletedTask;
        return _storedTasks.Values.Select(v => v.model);
    }

    public async Task<TaskModel?> GetAsync(Guid id)
    {
        await Task.CompletedTask;
        return _storedTasks.TryGetValue(id, out StoredTask? value) ? value?.model : null;
    }

    public async Task<TaskModel> CreateAsync(TaskModel model)
    {
        string? name = Convert.ToString(model.TaskType);
        var task = _taskFactory.Build(name!);
        if (task is null)
        {
            throw new ArgumentNullException($"The task type '{model.TaskType}' is not correct.", nameof(model));
        }

        var cancelToken = new CancellationTokenSource();
        await task.RunAsync(cancelToken.Token);

        model.Status = TaskStatusModel.Processing;
        _storedTasks.TryAdd(model.Id, new StoredTask(model, cancelToken));
        return model;
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        if(!_storedTasks.TryGetValue(id, out StoredTask? value) || value is null)
        {
            return false;
        }

        value.cancelToken.Cancel();
        await Task.CompletedTask;
        return true;        
    }
}
