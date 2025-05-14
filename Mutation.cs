namespace HangfireDemo;
public class Mutation
{
    public bool TestMutate([Service] VehicleService vehicleService) =>    
        vehicleService.WriteToConsole();
    
}
