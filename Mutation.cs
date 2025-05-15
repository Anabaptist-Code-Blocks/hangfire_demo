namespace HangfireDemo;

public class Mutation
{
    public bool Enqueue([Service] VehicleService vehicleService) =>    
         vehicleService.Enqueue();

    public bool Schedule([Service] VehicleService vehicleService) =>
        vehicleService.Schedule();

    public bool AddRecurring([Service] VehicleService vehicleService) =>
        vehicleService.AddRecurring();

    public bool AddContinueWith([Service] VehicleService vehicleService) =>
        vehicleService.AddContinueWith();
}
