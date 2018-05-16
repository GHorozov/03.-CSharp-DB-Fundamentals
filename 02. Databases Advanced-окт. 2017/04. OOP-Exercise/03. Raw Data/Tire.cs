public class Tire
{
    private double pressure;
    private int age;

    public Tire(double pressure, int age)
    {
        this.Pressure = pressure;
        this.Age = age;
    }

    public double Pressure { get => pressure; set => pressure = value; }
    public int Age { get => age; set => age = value; }
}
