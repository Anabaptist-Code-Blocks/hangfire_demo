﻿namespace HangfireDemo.GraphQL;

public class Mutation
{
    public bool Enqueue([Service] BackgroundJobService vehicleService)
    {
        vehicleService.Enqueue();
        return true;
    }

    public bool EnqueueSingle([Service] BackgroundJobService vehicleService, int count)
    {
        vehicleService.Enqueue(count);
        return true;
    }

    public bool Requeue([Service] BackgroundJobService vehicleService, string jobId)
    {
        vehicleService.Requeue(jobId);
        return true;
    }

    public bool Schedule([Service] BackgroundJobService vehicleService, int seconds)
    {
        vehicleService.Schedule(seconds);
        return true;
    }

    public bool Reschedule([Service] BackgroundJobService vehicleService, string jobId, int seconds)
    {
        vehicleService.Reschedule(jobId, seconds);
        return true;
    }

    public bool AddRecurring([Service] BackgroundJobService vehicleService, string cron)
    {
        vehicleService.AddRecurring(cron);
        return true;
    }

    public bool TriggerRecurring([Service] BackgroundJobService vehicleService)
    {
        vehicleService.TriggerRecurring();
        return true;
    }

    public bool RemoveRecurring([Service] BackgroundJobService vehicleService)
    {
        vehicleService.RemoveRecurring();
        return true;
    }

    public bool AddContinueWith([Service] BackgroundJobService vehicleService)
    {
        vehicleService.AddContinueWith();
        return true;
    }

    public bool TriggerBulkExample([Service] BulkExampleService bulkExampleService, int count)
    {
        bulkExampleService.UploadBulk(count);
        return true;
    }

    public bool TriggerUploadWithFilter([Service] BulkExampleService bulkExampleService, string text)
    {
        bulkExampleService.UploadWithFilterA(text);
        return true;
    }

}
