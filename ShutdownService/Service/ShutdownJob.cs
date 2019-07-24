using System.Threading.Tasks;
using Quartz;
using Serilog;

namespace Shutdown_Remote_Pc.Service
{
    public class ShutdownJob:IJob
    {
        public Task Execute(IJobExecutionContext context)
        {
            var lastRun = context.PreviousFireTimeUtc?.DateTime.ToString() ?? string.Empty;
            Log.Warning("Execute Job!Last previous run: {0}",lastRun);
            return Task.CompletedTask;
        }
    }
}