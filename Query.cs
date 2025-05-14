namespace HangfireDemo;
public class Query
{
    public Vehicle GetVehicle =>
        new Vehicle
        {
            Year = 2018,
            Brand = "Subaru",
            Model = "Legacy",
            Driver = new Driver
            {
                FirstName = "Leslie",
                LastName = "Martin"
            }
        };
}
