using System;
using System.Linq;
using P02_DatabaseFirst.Data;
using P02_DatabaseFirst.Data.Models;

namespace P07_EmployeesPr
{
    class StartUp
    {
        static void Main(string[] args)
        {
            var db = new SoftUniContext();

            using (db)
            {
                var employees = db.Employees
                    .Where(e => e.EmployeeProjects
                    .Any(p => p.Project.StartDate.Date.Year >= 2001 && p.Project.StartDate.Year <= 2003))
                    .Take(30)
                    .Select(e => new
                    {
                        Name = $"{e.FirstName} {e.LastName}",
                        Manager = $"{e.Manager.FirstName} {e.Manager.LastName}",
                        Projects = e.EmployeeProjects.Select(ep => new
                        {
                            ProjectName = ep.Project.Name,
                            ProjectStartDate = ep.Project.StartDate,
                            ProjectEndDate = ep.Project.EndDate == null ? "not finished" : $"{ep.Project.EndDate}"
                        })
                    });

                foreach (var e in employees)
                {
                    Console.WriteLine($"{e.Name} - Manager: {e.Manager}");

                    foreach (var p in e.Projects)
                    {
                        Console.WriteLine($"--{p.ProjectName} - {p.ProjectStartDate} - {p.ProjectEndDate}");
                    }
                }
            }
        }
    }
}
