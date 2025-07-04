﻿namespace HangfireDemo.GraphQL;
public class Subscription
{
    [Subscribe]
    [Topic("jobResult")]
    public JobResult OnJobUpdate([EventMessage] JobResult result) =>
        result;

}
