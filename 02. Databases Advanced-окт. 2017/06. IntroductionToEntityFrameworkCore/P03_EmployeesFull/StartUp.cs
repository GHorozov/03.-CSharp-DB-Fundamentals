using System;
using System.Collections.Generic;
using System.Linq;
using P02_DatabaseFirst.Data;
using P02_DatabaseFirst.Data.Models;

namespace P03_EmployeesFull
{
    class StartUp
    {
        static void Main(string[] args)
        {
            var db = new SoftUniContext();

            using (db)
            {
                var result = db.Employees
                    .OrderBy(e => e.EmployeeId)
                    .Select(e => new
                {
                    e.FirstName,
                    e.LastName,
                    e.MiddleName,
                    e.JobTitle,
                    e.Salary
                })
                .ToList();

                foreach (var item in result)
                {
                    Console.WriteLine($"{item.FirstName} {item.LastName} {item.MiddleName} {item.JobTitle} {item.Salary:f2}");
                }
            } 
        }
    }
}
