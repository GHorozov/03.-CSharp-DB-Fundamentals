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
        var people = new List<Person>();
        for (int i = 0; i < n; i++)
        {
            var input = Console.ReadLine().Split(' ');
            people.Add(new Person(input[0], int.Parse(input[1])));
        }

        foreach (var person in people.Where(x => x.Age > 30).OrderBy(x => x.Name))
        {
            Console.WriteLine(person.Name + " - " + person.Age);
        }
    }
}

