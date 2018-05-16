using System;
using System.Linq;
using P02_DatabaseFirst.Data;
using P02_DatabaseFirst.Data.Models;

namespace P13_FindEmployee
{
    class StartUp
    {
        static void Main(string[] args)
        {
            var db =new SoftUniContext();

            using (db)
            {
                var employees = db.Employees
                    .Where(e => e.FirstName.StartsWith("Sa"))
                    .OrderBy(e => e.FirstName).ThenBy(e => e.LastName)
                    .Select(e => new
                    {
                        e.FirstName,
                        e.LastName,
                        e.JobTitle,
                        e.Salary
                    });

                foreach (var e in employees)
                {
                    Console.WriteLine($"{e.FirstName} {e.LastName} - {e.JobTitle} - (${e.Salary:f2})");
                }
            }   
        }
    }
}
