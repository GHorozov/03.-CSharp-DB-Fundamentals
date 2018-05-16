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
        var listOfEmployees = new List<Employee>();

        for (int i = 0; i < n; i++)
        {
            var input = Console.ReadLine().Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            var currentEmployee = new Employee(input[0], decimal.Parse(input[1]), input[2], input[3]);

            if(input.Length > 4)
            {
                if (input[4].Contains("@"))
                {
                    var email = input[4];
                    currentEmployee.Email = email;
                }
                else
                {
                    var age = int.Parse(input[4]);
                    currentEmployee.Age = age;
                }
            }

            if(input.Length > 5)
            {
                var age = int.Parse(input[5]);
                currentEmployee.Age = age;
            }

            listOfEmployees.Add(currentEmployee);
        }


        var deptHighestSalary = listOfEmployees.GroupBy(d => d.Department)
                                               .OrderByDescending(g => g.Average(s => s.Salary))
                                               .FirstOrDefault();
                                                

        Console.WriteLine($"Highest Average Salary: {deptHighestSalary.Key}");
        foreach (var person in deptHighestSalary.OrderByDescending(x => x.Salary))
        {
            Console.WriteLine($"{person.Name} {person.Salary:f2} {person.Email} {person.Age}");
        }
    }
}
