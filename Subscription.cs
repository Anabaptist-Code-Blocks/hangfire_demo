using HotChocolate.Subscriptions;

namespace HangfireDemo;

public class Subscription
{
    [Subscribe]
    [Topic("driver")]
    public Driver OnDriverUpdate([EventMessage] Driver driver) =>
        driver;

}
