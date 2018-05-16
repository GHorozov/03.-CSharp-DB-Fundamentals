using System.Collections.Generic;

public class Car
{
    private string model;
    private Engine engine;
    private Cargo cargo;
    private List<Tire> tire;

    public Car(string model, Engine engine, Cargo cargo)
    {
        this.Model = model;
        this.Engine = engine;
        this.Cargo = cargo;
        this.Tire = new List<Tire>();
    }

    public string Model { get => model; set => model = value; }
    public Engine Engine { get => engine; set => engine = value; }
    public Cargo Cargo { get => cargo; set => cargo = value; }
    public List<Tire> Tire { get => tire; set => tire = value; }


    public override string ToString()
    {
        return $"{this.model}";
    }
}

