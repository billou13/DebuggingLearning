using System;
using System.Collections.Generic;
using System.Net.Mime;
using System.Threading.Tasks;
using DebuggingLearning.WebApi.Models;
using DebuggingLearning.WebApi.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DebuggingLearning.WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class TasksController : ControllerBase
{
    private readonly ITaskService _taskService;

    public TasksController(ITaskService taskService)
    {
        _taskService = taskService;
    }

    [HttpGet]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType<IEnumerable<TaskModel>>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetAll()
    {
        var models = await _taskService.GetAllAsync();
        return Ok(models);
    }

    [HttpGet("{id}")]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType<TaskModel>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Get(Guid id)
    {
        var model = await _taskService.GetAsync(id);
        return model is not null ? Ok(model) : NotFound();
    }

    [HttpPost]
    [Consumes(MediaTypeNames.Application.Json)]
    [ProducesResponseType<TaskModel>(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Create(TaskModel model)
    {
        var existingModel = await _taskService.GetAsync(model.Id);
        if (existingModel is not null)
        {
            return Conflict();
        }

        var createdModel = await _taskService.CreateAsync(model);
        return CreatedAtAction(nameof(Get), new { id = createdModel.Id }, createdModel);
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Delete(Guid id)
    {
        bool found = await _taskService.DeleteAsync(id);
        return found ? NoContent() : NotFound();
    }
}
