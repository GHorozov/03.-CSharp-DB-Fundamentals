public class Car
{
    private string model;
    private double fuelAmount;
    private double fuelConsumption;

    private double distanceTraveled = 0;

    public Car(string model, double fuelAmount, double fuelConsumption)
    {
        this.Model = model;
        this.FuelAmount = fuelAmount;
        this.fuelConsumption = fuelConsumption;
    }

    public string Model { get => model; set => model = value; }
    public double FuelAmount { get => fuelAmount; set => fuelAmount = value; }
    public double FuelConsumption { get => fuelConsumption; set => fuelConsumption = value; }
    public double DistanceTraveled { get => distanceTraveled; set => distanceTraveled = value; }


    public string Drive( string carModel, double kmToTravel)
    {
        var fuelForJourney = kmToTravel * FuelConsumption;

        if(this.fuelAmount >= fuelForJourney)
        {
            this.fuelAmount -= fuelForJourney;
            this.distanceTraveled += kmToTravel;
        }
        else
        {
            return "Insufficient fuel for the drive";
        }

        return null;
    }

    public override string ToString()
    {
        return $"{this.model} {this.fuelAmount:f2} {this.distanceTraveled}";
    }
}

