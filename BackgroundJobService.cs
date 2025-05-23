using Hangfire;
using HotChocolate.Subscriptions;

namespace HangfireDemo;
public class BackgroundJobService(ITopicEventSender eventSender)
{
    /*
     * ENQUEUED JOBS - Fire and Forget
     * Will be executed almost immediately
     */

    public void Enqueue()
    {
        //While this looks like a method call, its actually an expression tree. All its really doing is serializing the information
        //namespace, method name, arguments and argument types, etc.
        BackgroundJob.Enqueue(() => TestJob("Testing an enqueued job"));

    }

    //public void Enqueue()
    //{
    //    BackgroundJob.Enqueue(Constants.SlowQueue, () => TestJob("Testing an enqueued job"));
    //}

    public void Requeue(string jobId)
    {
        BackgroundJob.Requeue(jobId);
    }


    /*
     * SCHEDULED JOB
     * Will enqueue after the given delay
     * Server's default pl
     */
    public void Schedule(int seconds)   
    {
        var when = TimeSpan.FromSeconds(seconds);
        var jobId = BackgroundJob.Schedule(() => TestJob("Testing a schedule job"), when);
        Console.WriteLine($"The id for the schedule job is {jobId}");     
    }


    //Very similar to requeue
    public void Reschedule(string jobId, int seconds)
    {
        var when = TimeSpan.FromSeconds(seconds);
        var success = BackgroundJob.Reschedule(jobId, when);
    }



    /*
     * RECURRING JOBS
     * You need to assign an id to the job
     * And uses a Cron expression 
     * 
     * Adds records in the hash table
     * 
     * Note the id (key). Hangfire assigns its own id when the job is enqueued
     */

    private const string recurringJobId = "myRecurringJob";
    public void AddRecurring(string cronExpression)
    {
        //var when = "0 8 * * * ";
        //cron minute/hour/dayOfMonth/month/DayOfWeek

        RecurringJob.AddOrUpdate(recurringJobId, () => TestJob("This is a recurring job"), cronExpression, new RecurringJobOptions()
        {
            MisfireHandling = MisfireHandlingMode.Strict         
        });
    }

    //public void AddRecurring(string cronExpression)
    //{
    //    //var when = "0 8 * * * ";
    //    RecurringJob.AddOrUpdate(recurringJobId, Constants.SlowQueue, () => TestJob("This is a recurring job"), cronExpression, new RecurringJobOptions()
    //    {
    //        MisfireHandling = MisfireHandlingMode.Strict
    //    });
    //}

    //Can be triggered manually at any point with the recurring job id.
    //An example of this would be if you have a function scheduled to run at a certain time of night
    //but you also want to be able to run it manually at any time.
    public void TriggerRecurring()
    {
        RecurringJob.TriggerJob(recurringJobId);
                
    }

    public void RemoveRecurring()
    {
        RecurringJob.RemoveIfExists(recurringJobId);
    }




    /*
     * CONTINUE WITH 
     * These jobs will run as soon as their parent job is complete
     * So you can chain them in a specific order
     * 
     * Continue With does not work directly with Recurring jobs because a job instance doesn't exist for a recurring job until its enqueued
     */
    public void AddContinueWith()
    {
        var parentId = BackgroundJob.Enqueue(() => TestJob("Enqueueing parent in Continue with"));

        BackgroundJob.ContinueJobWith(parentId, Constants.DefaultQueue, () => TestJob("Enqueueing child job in Continue with"), JobContinuationOptions.OnlyOnSucceededState);       
    }




    /*
     * Methods must be public so the hangfire background threads call them
     * It is best practice to pass in Id's as arguments rather than entire objects
     */

    //[Queue(Constants.FastQueue)]
    public async Task TestJob(string text)
    {
        await Task.Delay(5000);

        await eventSender.SendAsync("jobResult", new JobResult() { Text = text, Random = new Random()});
    }








    /*
     Explore the API a bit
     */
    public void GetStatistics()
    {
        var storage = JobStorage.Current;
        var monitoring = storage.GetMonitoringApi();

        var statistics = monitoring.GetStatistics();
        
        var processingCount = monitoring.ProcessingCount();
        var processing = monitoring.ProcessingJobs(0, int.MaxValue);

        var enqueued = monitoring.EnqueuedJobs(Constants.DefaultQueue, 0, int.MaxValue);    
    
    }



    public string? GetJObDetails(string jobId)
    {
        var details = JobStorage.Current.GetMonitoringApi().JobDetails(jobId);

        var job = details.Job;

        return job.Method.Name;
    }
    

}