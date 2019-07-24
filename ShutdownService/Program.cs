using Serilog;
using Topshelf;
using System.IO;
using System;
namespace Shutdown_Remote_Pc
{
    class Program
    {
        static void Main(string[] args)
        {
            var rc = HostFactory.Run(x =>
            {
                x.Service<Service.SchedulerJob>(s =>
                {
                    s.ConstructUsing(name => new Service.SchedulerJob());
                    s.WhenStarted(c => c.Start());
                    s.WhenStopped(c => c.Stop());
                });
                x.RunAsLocalSystem();

                Log.Logger = new LoggerConfiguration()
                                .MinimumLevel.Information()
                                .WriteTo.Console()
                                .WriteTo.File($"{Path.GetTempPath()}logs\\ShutdownServiceLogs.txt", rollingInterval: RollingInterval.Day)
                                .CreateLogger();
                x.UseSerilog(Log.Logger);

                x.SetDescription("Schedulerer shutdown computer on remote");
                x.SetDisplayName("Shutdown Service");
                x.SetServiceName("ShutdownSrvc");
            });

            var exitCode = (int)Convert.ChangeType(rc, rc.GetTypeCode());
            Environment.ExitCode = exitCode;
        }
    }
}
