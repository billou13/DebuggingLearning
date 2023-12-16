using DebuggingLearning.WebApi.Models;
using DebuggingLearning.WebApi.Services.Interfaces;
using Microsoft.Extensions.Logging;
using System;

namespace DebuggingLearning.WebApi.Services;

public class CommandService : ICommandService
{
    private readonly ILogger<CommandService> _logger;

    public CommandService(ILogger<CommandService> logger)
    {
        _logger = logger;
    }

    public void Execute(CommandTypeModel command)
    {
        _logger.LogDebug($"{nameof(Execute)} - running command '{command}'...");
        switch (command)
        {
            case CommandTypeModel.GcCollect: GC.Collect(); break;
            case CommandTypeModel.GcWaitForPendingFinalizers: GC.WaitForPendingFinalizers(); break;
            default: break;
        }

        _logger.LogInformation($"{nameof(Execute)} - command '{command}' executed.");
    }
}
