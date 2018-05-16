using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class StartUp
{
    public static void Main(string[] args)
    {

        var peopleInput = Console.ReadLine().Split(';').ToArray();
        var productsInput = Console.ReadLine().Split(';').ToArray();

        var peopleList = new List<Person>();
        var productsList = new List<Product>();

        foreach(var p in peopleInput)
        {
            var parts = p.Split('=').ToArray();
            var name = parts[0];
            var money = double.Parse(parts[1]);

            try
            {
                var currentPerson = new Person(name, money);
                peopleList.Add(currentPerson);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return;
            }

        }

        foreach(var pr in productsInput)
        {
            var parts = pr.Split('=').ToArray();

            if (parts.Length < 2) break;

            var name = parts[0];
            var price = double.Parse(parts[1]);
            try
            {
                var currentProduct = new Product(name, price);
                productsList.Add(currentProduct);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return;
            }
        }

        string command;
        while ((command = Console.ReadLine()) != "END")
        {
            var lineParts = command.Split();
            var peopleName = lineParts[0];
            var productName = lineParts[1];

            var currentManFromPeople = peopleList.First(x => x.Name == peopleName);
            var currentProductFromProducts = productsList.First(x => x.Name == productName);

            Console.WriteLine(currentManFromPeople.BuyProduct(currentManFromPeople, currentProductFromProducts));
        }

        foreach (var p in peopleList)
        {
            Console.WriteLine(p.BoughtProducts().TrimEnd(','));
        }

    }
}

