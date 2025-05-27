namespace HangfireDemo.GraphQL;
public class Query
{
    public bool GetStatistics([Service] BackgroundJobService service)
    {
        service.GetStatistics();
        return true;
    }

    public string? GetJobDetails([Service] BackgroundJobService service, string jobId) =>
        service.GetJObDetails(jobId);
}
