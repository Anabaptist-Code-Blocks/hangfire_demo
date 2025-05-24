using Hangfire.Client;
using Hangfire.Common;
using Hangfire.Server;
using Hangfire.States;
using Hangfire.Storage;

namespace HangfireDemo;

public class ExampleFilterAttribute : JobFilterAttribute, IClientFilter, IServerFilter, IElectStateFilter, IApplyStateFilter
{
    public void OnCreated(CreatedContext context)
    {
        throw new NotImplementedException();
    }

    public void OnCreating(CreatingContext context)
    {
        throw new NotImplementedException();
    }

    public void OnPerformed(PerformedContext context)
    {
        throw new NotImplementedException();
    }

    public void OnPerforming(PerformingContext context)
    {
        throw new NotImplementedException();
    }

    public void OnStateApplied(ApplyStateContext context, IWriteOnlyTransaction transaction)
    {
        throw new NotImplementedException();
    }

    public void OnStateElection(ElectStateContext context)
    {
        throw new NotImplementedException();
    }

    public void OnStateUnapplied(ApplyStateContext context, IWriteOnlyTransaction transaction)
    {
        throw new NotImplementedException();
    }
}

