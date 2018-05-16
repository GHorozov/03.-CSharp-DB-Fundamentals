using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class StartUp
{
    static void Main(string[] args)
    {
        var n = int.Parse(Console.ReadLine());
        var cars = new List<Car>();
        for (int i = 0; i < n; i++)
        {
            var input = Console.ReadLine().Split();
            var model = input[0];
            var engineSpeed = int.Parse(input[1]);
            var enginePower = int.Parse(input[2]);
            var cargoWeight = int.Parse(input[3]);
            var cargoType = input[4];
            var tire1Pressure = double.Parse(input[5]);
            var tire1Age = int.Parse(input[6]);
            var tire2Pressure = double.Parse(input[7]);
            var tire2Age = int.Parse(input[8]);
            var tire3Pressure = double.Parse(input[9]);
            var tire3Age = int.Parse(input[10]);
            var tire4Pressure = double.Parse(input[11]);
            var tire4Age = int.Parse(input[12]);

            var engine = new Engine(engineSpeed, enginePower);
            var cargo = new Cargo(cargoWeight, cargoType);

            Car car = new Car(model, engine, cargo);

            car.Tire.Add(new Tire(tire1Pressure, tire1Age));
            car.Tire.Add(new Tire(tire2Pressure, tire2Age));
            car.Tire.Add(new Tire(tire3Pressure, tire3Age));
            car.Tire.Add(new Tire(tire4Pressure, tire4Age));

            cars.Add(car);
        }

        var command = Console.ReadLine();

        if(command == "fragile")
        {
            cars
                .Where(c => c.Cargo.Type == "fragile")
                .Where(t => t.Tire.Any(tp => tp.Pressure < 1))
                .Select(m => m.Model)
                .ToList()
                .ForEach(x => Console.WriteLine(x));
          
        }
        else if(command == "flammable")
        {
            cars
                .Where(c => c.Cargo.Type == "flammable" && c.Engine.Power > 250)
                .Select(cm => cm.Model)
                .ToList()
                .ForEach(m => Console.WriteLine(m));
        }
    }
}
