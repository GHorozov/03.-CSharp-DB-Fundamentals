using System;
using System.Linq;
using P02_DatabaseFirst.Data;
using P02_DatabaseFirst.Data.Models;

namespace P09_Employee_147
{
    class StartUp
    {
        static void Main(string[] args)
        {
            var db = new SoftUniContext();

            using (db)
            {
                var emp = db.Employees
                    .Where(e => e.EmployeeId == 147)
                    .Select(e => new
                    {
                        Name = $"{e.FirstName} {e.LastName}",
                        JobTitle = $"{e.JobTitle}",
                        Projects = e.EmployeeProjects.Select(p => p.Project.Name).OrderBy(x => x)
                    });

                foreach (var e in emp)
                {
                    Console.WriteLine($"{e.Name} - {e.JobTitle}");

                    Console.WriteLine(string.Join(Environment.NewLine, e.Projects));
                }
            }
        }
    }
}
