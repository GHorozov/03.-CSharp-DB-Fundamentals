using System;
using System.Linq;
using P02_DatabaseFirst.Data;
using P02_DatabaseFirst.Data.Models;


namespace P10_DepartmentMore
{
    class StartUp
    {
        static void Main(string[] args)
        {
            var db = new SoftUniContext();

            using (db)
            {
                var deprtments = db.Departments
                    .Where(d => d.Employees.Count > 5)
                    .OrderBy(d => d.Employees.Count)
                    .ThenBy(d => d.Name)
                    .Select(d => new
                    {
                        DepartmentName = d.Name,
                        ManagerName = $"{d.Manager.FirstName} {d.Manager.LastName}",
                        EmployeeInfo = d.Employees.Select(e => new
                        {
                            e.FirstName,
                            e.LastName,
                            e.JobTitle
                        })
                    });

                foreach (var d in deprtments)
                {
                    Console.WriteLine($"{d.DepartmentName} - {d.ManagerName}");

                    foreach (var e in d.EmployeeInfo.OrderBy(x => x.FirstName).ThenBy(x => x.LastName))
                    {
                        Console.WriteLine($"{e.FirstName} {e.LastName} - {e.JobTitle}");
                    }

                    Console.WriteLine("----------");
                }

            }
        }
    }
}
