using Hangfire;
using Hangfire.Annotations;
using Hangfire.AspNetCore;

namespace HangfireDemo
{
    public class TestJobActivator : AspNetCoreJobActivator
    {
        public TestJobActivator([NotNull] IServiceScopeFactory serviceScopeFactory) : base(serviceScopeFactory)
        {
        }

        public override JobActivatorScope BeginScope(JobActivatorContext context)
        {
            var scope = base.BeginScope(context);

            return scope;
        }

    }
}
