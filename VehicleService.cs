using Hangfire;
using System.Threading.Tasks;

namespace HangfireDemo;
public class VehicleService
{
    /*
     * ENQUEUED JOBS
     * Will be executed almost immediately
     */
    public void Enqueue()
    {
        //While this looks like a method call, all its really doing is serializing the information
        //namespace, method name, arguments and argument types, etc.
        BackgroundJob.Enqueue(() => TestJob("Testing an enqueued job"));
      
    }

    /*
     * SCHEDULED JOB
     * Will enqueue after the given delay
     */
    public void Schedule()
    {

        var when = TimeSpan.FromSeconds(10);
        var jobId = BackgroundJob.Schedule(() => TestJob("Testing a schedule job"), when);
        Console.WriteLine($"The id for the schedule job is {jobId}");     
    }

    /*
     * RECURRING JOBS
     * You need to assign an id to the job
     * And uses a Cron expression  * 
     */

    private const string recurringJobId = "recurring_123";
    public void AddRecurring()
    {

        var when = "0 8 * * * ";
        RecurringJob.AddOrUpdate(recurringJobId, () => TestJob("This is a recurring job"), when);

    }

    //can be triggered manually at any point with the recurring job id.
    //An example of this would be if you have a function scheduled to run at a certain time of night
    //but you also want to be able to run it manually at any time.
    public void TriggerRecurring() =>
        RecurringJob.TriggerJob("recurring_123");



    /*
     * CONTINUE WITH 
     * These jobs will run as soon as their parent job is complete
     * So you can chain them in a specific order
     */
    public void AddContinueWith()
    {
        var parentId = BackgroundJob.Enqueue(() => TestJob("Enqueueing parent in Continue with"));
        BackgroundJob.ContinueJobWith(parentId, () => TestJob("Enqueueing child job in Continue with"));       
    }




    //Methods must be public so Hangfire can "see" them
    public async Task TestJob(string text)
    {
        Console.WriteLine(text);
        await Task.Delay(5000);
    }


    //Its best practice to pass ids in rather than objects, because of Hangfire's serialization
    public async Task UpdateDriverOnVehicle(long driverId)
    {
        //Look up the driver in your db.

    }


    //NOT RECOMMENDED
    // * Serialization problems
    //       -You may get a serialization exception or it may just act up
    // * You don't know if the object will be out of date till the job executes

    //public async Task UpdateDriverOnVehicle(Driver driver)
    //{


    //}

}

