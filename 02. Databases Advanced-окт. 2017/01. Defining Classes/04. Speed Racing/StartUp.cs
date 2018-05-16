using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class StartUp
{
    public static void Main(string[] args)
    {
        var n = int.Parse(Console.ReadLine());
        var cars = new List<Car>();
        for (int i = 0; i < n; i++)
        {
            var inputCars = Console.ReadLine().Split();
            cars.Add(new Car(inputCars[0], double.Parse(inputCars[1]), double.Parse(inputCars[2])));
        }

        string command;
        while ((command = Console.ReadLine()) != "End")
        {
            var lineParts = command.Split(' ');
            var model = lineParts[1];
            var kmToTravel = double.Parse(lineParts[2]);

            var result = cars.FirstOrDefault(x => x.Model == model).Drive(model, kmToTravel);

            if(result != null)
            {
                Console.WriteLine(result);
            }
        }

        foreach (var car in cars)
        {
            Console.WriteLine(car.ToString());
        }     
    }
}

