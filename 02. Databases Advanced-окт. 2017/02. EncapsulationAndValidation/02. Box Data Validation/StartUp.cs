using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

public class StartUp
{
    public static void Main(string[] args)
    {
        try
        {

            Type boxType = typeof(Box);
            FieldInfo[] fields = boxType.GetFields(BindingFlags.NonPublic | BindingFlags.Instance);
            Console.WriteLine(fields.Count());

            var length = double.Parse(Console.ReadLine());
            var width = double.Parse(Console.ReadLine());
            var height = double.Parse(Console.ReadLine());

            var box = new Box(length, width, height);


            Console.WriteLine($"Surface Area - {box.SurfaceArea(length, width, height):f2}");
            Console.WriteLine($"Lateral Surface Area - {box.LiteralSurfaceArea(length, width, height):f2}");
            Console.WriteLine($"Volume - {box.Volume(length, width, height):f2}");
        }
        catch (ArgumentException ex)
        {
            Console.WriteLine(ex.Message);
        }
    }
}
