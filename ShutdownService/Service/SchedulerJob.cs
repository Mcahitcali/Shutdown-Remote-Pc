using System.Collections.Specialized;
using Quartz;
using Quartz.Impl;

namespace Shutdown_Remote_Pc.Service
{
    public class SchedulerJob
    {
        private readonly IScheduler scheduler;
        public SchedulerJob()
        {
            NameValueCollection props = new NameValueCollection
            {
                { "quartz.serializer.type", "binary" },
                { "quartz.scheduler.instanceName", "MyScheduler" },
                { "quartz.jobStore.type", "Quartz.Simpl.RAMJobStore, Quartz" },
                { "quartz.threadPool.threadCount", "3" }
            };
            StdSchedulerFactory factory = new StdSchedulerFactory(props);
            scheduler = factory.GetScheduler().ConfigureAwait(false).GetAwaiter().GetResult();
        } 

        public void Start()
        {
            scheduler.Start().ConfigureAwait(false).GetAwaiter().GetResult();
            SchedulerInitialize();
        }

        public void Stop()
        {
            scheduler.Shutdown().ConfigureAwait(false).GetAwaiter().GetResult();
        }

        public void SchedulerInitialize()
        {
            IJobDetail job = JobBuilder.Create<ShutdownJob>()
                                        .WithIdentity("shutdownJob1","shutdownsch")
                                        .Build();
            ITrigger trigger = TriggerBuilder.Create()
                                .WithIdentity("shutdownTrigger1","shutdownsch")
                                .StartNow()
                                .WithSimpleSchedule(x => x
                                                    .WithIntervalInSeconds(10)
                                                    .RepeatForever())
                                .Build();

            scheduler.ScheduleJob(job,trigger).ConfigureAwait(false).GetAwaiter().GetResult();
        }
    }
}