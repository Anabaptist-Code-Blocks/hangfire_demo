using Hangfire;
using HotChocolate.Subscriptions;

namespace HangfireDemo;

public class BulkExampleService(ITopicEventSender eventSender)
{

    public void UploadBulk(int count)
    {
        for (int i = 0; i < count; i++)
        {
            BackgroundJob.Enqueue(() => Upload(i.ToString()));
        }
    }


    //public void UploadBulk(int count)
    //{
    //    for (int i = 0; i < count; i++)
    //    {
    //        //Once every 2 seconds
    //        var when = TimeSpan.FromSeconds(i * 2);
    //        BackgroundJob.Schedule(Constants.FastQueue, () => Upload(i.ToString()), when);
    //    }
    //}



    //public void UploadBulk(int count)
    //{
    //    string jobId = string.Empty;
    //    for(int i = 0; i < count; i++)
    //    {
    //        // 5 per second
    //        var when = TimeSpan.FromSeconds(i / 5);
    //        jobId = BackgroundJob.Schedule(Constants.FastQueue, () => Upload(i.ToString()), when);
    //    }
    //    BackgroundJob.ContinueJobWith(jobId, Constants.DefaultQueue, () => Upload("Completed"));
    //}



    //[AutomaticRetry(Attempts = 3, DelaysInSeconds = [15], OnAttemptsExceeded = AttemptsExceededAction.Fail)]
    [DisableConcurrentExecution(30)] 
    public async Task Upload(string text)
    {
        await Task.Delay(5000);

        await eventSender.SendAsync("jobResult", new JobResult() { Text = text, Random = new Random() });




        /*
         * Exception Handling
         */

        //Just throw
        //Will automatically retry
        //if (text == "99")
        //    throw new Exception("We are at 99");



        //In a try/catch
        //Hangfire does not know anything went wrong.
        //try
        //{
        //    if (text == "99")
        //        throw new Exception("We are at 99");
        //}
        //catch (Exception ex)
        //{
        //    Console.WriteLine($"ERROR: {ex.Message}");
        //}


    }





    public void UploadWithFilterA(string text)
    {
        BackgroundJob.Enqueue(() => UploadWithFilterB(text));

    }


    [ExampleFilter]
    public async Task UploadWithFilterB(string text)
    {
        await Task.Delay(5000);

        await eventSender.SendAsync("jobResult", new JobResult() { Text = text, Random = new Random() });
    }



}
