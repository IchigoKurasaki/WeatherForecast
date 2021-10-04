using Quartz;
using Quartz.Impl;

namespace WeatherForecast.Parser
{
    public class JobScheduler
    {
        public async void Start()
        {
            ISchedulerFactory schedulerFactory = new StdSchedulerFactory();
            IScheduler scheduler = await schedulerFactory.GetScheduler();
            await scheduler.Start();

            var job = BuildJob<ParserJob>();
            var trigger = BuildTrigger();

            await scheduler.ScheduleJob(job, trigger);
        }

        private IJobDetail BuildJob<T>() where T : IJob
        {
            return JobBuilder.Create<T>()
                .WithIdentity("ParserJob", "WeatherForecastGroup")
                .Build();
        }

        private ITrigger BuildTrigger()
        {
            return TriggerBuilder.Create()
                .WithIdentity("ParserJobTrigger", "WeatherForecastGroup")
                .StartNow()
                .WithSimpleSchedule(x => x.WithIntervalInHours(6).RepeatForever())
                .Build();
        }
    }
}
