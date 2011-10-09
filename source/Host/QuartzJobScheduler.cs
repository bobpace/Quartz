using System;
using Common.Logging;
using Quartz;
using Quartz.Impl;
using Quartz.Spi;

namespace Host
{
    public class QuartzJobScheduler
    {
        readonly ILog _log = LogManager.GetCurrentClassLogger();
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
            trigger.StartTimeUtc = DateTime.UtcNow.AddSeconds(1);
            trigger.Name = "testTrigger";
            var jobDetail = new JobDetail("myJob", typeof(TestJob));
            _log.Debug("Scheduling job");
            _scheduler.ScheduleJob(jobDetail, trigger);
        }
    }
    
    public class TriggerListener : ITriggerListener
    {
        public void TriggerFired(Trigger trigger, JobExecutionContext context)
        {
            throw new NotImplementedException();
        }

        public bool VetoJobExecution(Trigger trigger, JobExecutionContext context)
        {
            throw new NotImplementedException();
        }

        public void TriggerMisfired(Trigger trigger)
        {
            throw new NotImplementedException();
        }

        public void TriggerComplete(Trigger trigger, JobExecutionContext context, SchedulerInstruction triggerInstructionCode)
        {
            throw new NotImplementedException();
        }

        public string Name
        {
            get { throw new NotImplementedException(); }
        }
    }

    public class JobListener : IJobListener
    {
        public void JobToBeExecuted(JobExecutionContext context)
        {
            throw new NotImplementedException();
        }

        public void JobExecutionVetoed(JobExecutionContext context)
        {
            throw new NotImplementedException();
        }

        public void JobWasExecuted(JobExecutionContext context, JobExecutionException jobException)
        {
            throw new NotImplementedException();
        }

        public string Name
        {
            get { throw new NotImplementedException(); }
        }
    }

    public class Plugin : ISchedulerPlugin
    {
        readonly TriggerListener _triggerListener;
        readonly JobListener _jobListener;

        public Plugin()
        {
            _triggerListener = new TriggerListener();
            _jobListener = new JobListener();
        }

        public void Initialize(string pluginName, IScheduler sched)
        {
            sched.AddTriggerListener(_triggerListener);
            sched.AddJobListener(_jobListener);
        }

        public void Start()
        {
        }

        public void Shutdown()
        {
        }
    }

    public class TestJob : IJob
    {
        readonly ILog _log = LogManager.GetCurrentClassLogger();

        public void Execute(JobExecutionContext context)
        {
            try
            {
                _log.Debug("Executing test job");
            }
            catch (Exception ex)
            {
                throw new JobExecutionException(ex);
            }
        }
    }
}