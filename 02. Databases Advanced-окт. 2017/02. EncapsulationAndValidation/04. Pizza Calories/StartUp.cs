using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class StartUp
{
    static void Main(string[] args)
    {
        try
        {
            var inputInfo = Console.ReadLine().Split();
            var pizzaName = inputInfo[1];

            var pizza = new Pizza(pizzaName);

            var inputDoughInfo = Console.ReadLine().Split();
            var pizzaDough = new Dough(inputDoughInfo[1], inputDoughInfo[2], double.Parse(inputDoughInfo[3]));
            pizza.Dough = pizzaDough;

            string inputToppingInfo;
            while ((inputToppingInfo = Console.ReadLine()) != "END")
            {
                var parts = inputToppingInfo.Split();
                var currentTopping = new Topping(parts[1], double.Parse(parts[2]));

                pizza.AddToppings(currentTopping);
            }

            Console.WriteLine($"{pizza.Name} - {pizza.TotalCaloriesInPizza():f2} Calories.");
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message); return;
        }
    }
}

