using Hangfire.Client;

namespace HangfireDemo
{
    public class ExampleJobFilter : IClientFilter
    {
        public void OnCreated(CreatedContext context)
        {
            Console.WriteLine($"Created job for {context.Job.Method.Name}");
        }

        public void OnCreating(CreatingContext context)
        {
            Console.WriteLine($"Creating job for {context.Job.Method.Name}");
        }
    }
}
