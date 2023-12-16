using System.Threading;
using System.Threading.Tasks;

namespace DebuggingLearning.Tasks.Interfaces;

public interface ITask
{
    Task RunAsync(CancellationToken token);
}
