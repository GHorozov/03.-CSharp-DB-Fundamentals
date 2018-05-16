using System;
using System.Linq;
using P02_DatabaseFirst.Data;
using P02_DatabaseFirst.Data.Models;

namespace P14_DeleteProject
{
    class StartUp
    {
        static void Main(string[] args)
        {
            var db = new SoftUniContext();

            var projectId = 2;

            using (db)
            {
                var empProject = db.EmployeesProjects
                    .Where(p => p.ProjectId == projectId);

                foreach (var p in empProject)
                {
                    db.EmployeesProjects.Remove(p);
                }

                var project = db.Projects.Find(2);
                db.Remove(project);

                db.SaveChanges();

                var projectsResult = db.Projects
                    .Take(10)
                    .Select(p => p.Name);

                foreach (var p in projectsResult)
                {
                    Console.WriteLine(p);
                }
            }
        }
    }
}
