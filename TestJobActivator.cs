using Hangfire;
using Hangfire.AspNetCore;

namespace HangfireDemo
{
    public class TestJobActivator(IServiceScopeFactory serviceScopeFactory) : AspNetCoreJobActivator(serviceScopeFactory)
    {
        //Custom job activator to instantiate the target types before invoking the methods
        //Need to register it in program.cs

        public override JobActivatorScope BeginScope(JobActivatorContext context)
        {
            //manipulate stuff on the scope
            var scope = base.BeginScope(context);

            var service = (MyOtherService)scope.Resolve(typeof(MyOtherService));

            return scope;
        }


        public override object ActivateJob(Type jobType)
        {
            return base.ActivateJob(jobType);
        }
    }
}
