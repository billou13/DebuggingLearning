using DebuggingLearning.WebApi.Models;

namespace DebuggingLearning.WebApi.Services.Interfaces;

public interface ICommandService
{
    void Execute(CommandTypeModel command);
}
