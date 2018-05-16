using System;
using System.Linq;
using P02_DatabaseFirst.Data;
using P02_DatabaseFirst.Data.Models;

namespace P12_IncreaseSalary
{
    class StartUp
    {
        static void Main(string[] args)
        {
            var db = new SoftUniContext();

            using (db)
            {
                var employees = db.Employees // 12.
                    .Where(e => e.Department.Name == "Engineering" ||
                                 e.Department.Name == "Tool Design" ||
                                  e.Department.Name == "Marketing" ||
                                   e.Department.Name == "Information Services")
                    .OrderBy(e => e.FirstName).ThenBy(e => e.LastName)
                    .ToList();
                foreach (var e in employees)
                {
                    e.Salary *= 1.12M;
                    Console.WriteLine($"{e.FirstName} {e.LastName} (${e.Salary:f2})");
                }
                db.SaveChanges();
            }
        }
    }
}
