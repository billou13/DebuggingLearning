using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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
        _logger.LogDebug($"{nameof(GetAllAsync)} - loading all tasks...");

        await Task.CompletedTask;
        var models = _storedTasks.Values.Select(v => v.model);

        _logger.LogInformation($"{nameof(GetAllAsync)} - {models.Count()} tasks loaded.");
        return models;
    }

    public async Task<TaskModel?> GetAsync(Guid id)
    {
        _logger.LogDebug($"{nameof(GetAsync)} - loading task '{id}'...");

        await Task.CompletedTask;
        if (!_storedTasks.TryGetValue(id, out StoredTask? value))
        {
            _logger.LogWarning($"{nameof(GetAsync)} - unable to load task '{id}' because it does not exist.");
            return null;
        }

        var model = value!.model;

        _logger.LogInformation($"{nameof(GetAsync)} - task '{model.Id}' loaded: type='{model.TaskType}', status='{model.Status}'.");
        return model;
    }

    public async Task<TaskModel> CreateAsync(TaskModel model)
    {
        _logger.LogDebug($"{nameof(CreateAsync)} - creating task '{model.Id}': type='{model.TaskType}'...");

        string? name = Convert.ToString(model.TaskType);
        var task = _taskFactory.Build(name!);
        if (task is null)
        {
            _logger.LogError($"{nameof(CreateAsync)} - unable to create task '{model.Id}' because type '{model.TaskType}' is not recognized.");
            throw new ArgumentNullException($"The task type '{model.TaskType}' is not correct.", nameof(model));
        }

        var cancelToken = new CancellationTokenSource();
        await task.RunAsync(cancelToken.Token);

        model.Status = TaskStatusModel.Processing;
        _storedTasks.TryAdd(model.Id, new StoredTask(model, cancelToken));

        _logger.LogInformation($"{nameof(CreateAsync)} - task '{model.Id}' created: type='{model.TaskType}', status='{model.Status}'.");
        return model;
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        _logger.LogDebug($"{nameof(DeleteAsync)} - deleting task '{id}'...");
        if (!_storedTasks.TryRemove(id, out StoredTask? value) || value is null)
        {
            _logger.LogWarning($"{nameof(DeleteAsync)} - unable to delete task '{id}' because it does not exist.");
            return false;
        }

        var model = value!.model;
        value.cancelToken.Cancel();
        await Task.CompletedTask;
        
        _logger.LogInformation($"{nameof(DeleteAsync)} - task '{model.Id}' deleted: type='{model.TaskType}', status='{model.Status}'.");
        return true;        
    }
}
