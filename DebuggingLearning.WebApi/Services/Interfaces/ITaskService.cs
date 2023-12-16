using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DebuggingLearning.WebApi.Models;

namespace DebuggingLearning.WebApi.Services.Interfaces;

public interface ITaskService
{
    Task<IEnumerable<TaskModel>> GetAllAsync();
    Task<TaskModel?> GetAsync(Guid id);
    Task<TaskModel> CreateAsync(TaskModel model);
    Task<bool> DeleteAsync(Guid id);
}
