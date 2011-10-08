using System;
using Quartz;
using Quartz.Impl;

namespace Host
{
    public class QuartzJobScheduler
    {
        readonly ISchedulerFactory _factory;
        readonly IScheduler _scheduler;

        public QuartzJobScheduler()
        {
            _factory = new StdSchedulerFactory();
            _scheduler = _factory.GetScheduler();
        }

        public void Run()
        {
            var trigger = TriggerUtils.MakeSecondlyTrigger(1, 5);
            trigger.StartTimeUtc = DateTime.UtcNow.AddSeconds(5);
            trigger.Name = "testTrigger";
            var jobDetail = new JobDetail("myJob", typeof(TestJob));
            _scheduler.ScheduleJob(jobDetail, trigger);
        }
    }

    public class TestJob : IJob
    {
        public void Execute(JobExecutionContext context)
        {
            Console.WriteLine("hello!");
        }
    }
}