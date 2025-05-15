namespace HangfireDemo;
public class SimulatedApisService
{
    
    //To simulate updating an object on an external api
    public async Task <bool> PushVehicle(Vehicle vehicle)
    {
        await Task.Delay(500);
        return true;
    }

    public async Task<bool> GetVehicles()
    {

    }
}
