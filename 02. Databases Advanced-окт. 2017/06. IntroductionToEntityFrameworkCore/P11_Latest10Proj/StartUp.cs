using System;
using System.Linq;
using P02_DatabaseFirst.Data;
using P02_DatabaseFirst.Data.Models;

namespace P11_Latest10Proj
{
    class StartUp
    {
        static void Main(string[] args)
        {
            var db = new SoftUniContext();

            using (db)
            {
                var projects = db.Projects
                    .OrderByDescending(p => p.StartDate)
                    .Take(10)
                    .OrderBy(p => p.Name)
                    .Select(p => new
                    {
                        Name = p.Name,
                        Description = p.Description,
                        StartDate = p.StartDate
                    });

                foreach (var proj in projects)
                {
                    Console.WriteLine(proj.Name);
                    Console.WriteLine(proj.Description);
                    Console.WriteLine(proj.StartDate);
                }
            }
        }
    }
}
