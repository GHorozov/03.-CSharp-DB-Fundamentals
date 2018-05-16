using System;
using System.Reflection;

public class StartUp
{
    public static void Main(string[] args)
    {
        Type personType = typeof(Person);
        PropertyInfo[] properties = personType.GetProperties
            (BindingFlags.Public | BindingFlags.Instance);
        Console.WriteLine(properties.Length);

        //var input = Console.ReadLine();
        var pesho = new Person("Pesho", 20);
        var gosho = new Person("Gosho", 18);
        var stamat = new Person("Stamat", 43);
    }
}

