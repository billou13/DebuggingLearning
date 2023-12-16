using System.Net.Mime;
using DebuggingLearning.WebApi.Models;
using DebuggingLearning.WebApi.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DebuggingLearning.WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class CommandController : ControllerBase
{
    private readonly ICommandService _commandService;

    public CommandController(ICommandService commandService)
    {
        _commandService = commandService;
    }

    [HttpPost]
    [Consumes(MediaTypeNames.Application.Json)]
    [ProducesResponseType<CommandModel>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public IActionResult Create(CommandModel model)
    {
        _commandService.Execute(model.CommandType);
        return Ok(model);
    }
}
