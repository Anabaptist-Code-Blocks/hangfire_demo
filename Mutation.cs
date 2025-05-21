namespace HangfireDemo;

public class Mutation
{
    public bool Enqueue([Service] VehicleService vehicleService)
    {
        vehicleService.Enqueue();
        return true;
    }

    public bool Schedule([Service] VehicleService vehicleService)
    {
        vehicleService.Schedule();
        return true;
    }

    public bool AddRecurring([Service] VehicleService vehicleService)
    {
        vehicleService.AddRecurring();
        return true;
    }

    public bool AddContinueWith([Service] VehicleService vehicleService)
    {
        vehicleService.AddContinueWith();
        return true;
    }
}
