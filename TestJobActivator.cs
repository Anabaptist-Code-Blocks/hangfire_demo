using Hangfire;
using Hangfire.Annotations;
using Hangfire.AspNetCore;

namespace HangfireDemo
{
    public class TestJobActivator([NotNull] IServiceScopeFactory serviceScopeFactory) : AspNetCoreJobActivator(serviceScopeFactory)
    {
        public override JobActivatorScope BeginScope(JobActivatorContext context)
        {
            var scope = base.BeginScope(context);

            return scope;
        }
    }
}
