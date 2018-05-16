using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

public class StartUp
{
    public static void Main()
    {
        MethodInfo oldestMemberMethod = typeof(Family).GetMethod("GetOldestMember");
        MethodInfo addMemberMethod = typeof(Family).GetMethod("AddMember");
        if (oldestMemberMethod == null || addMemberMethod == null)
        {
            throw new Exception();
        }

        var n = int.Parse(Console.ReadLine());
        var family = new Family();
        for (int i = 0; i < n; i++)
        {
            var lineParts = Console.ReadLine().Split();
            var name = lineParts[0];
            var age = int.Parse(lineParts[1]);

            var currentPerson = new Person(name, age);
            family.AddMember(currentPerson);
        }

        var oldestMember = family.GetOldestMember();
        Console.WriteLine($"{oldestMember.Name} {oldestMember.Age}");
    }
}