using System;
using System.Linq;
using P02_DatabaseFirst.Data;

namespace P05_Research
{
    class StartUp
    {
        static void Main(string[] args)
        {
            var db = new SoftUniContext();

            using (db)
            {
                var result = db.Employees
                    .Where(d => d.Department.Name == "Research and Development")
                    .OrderBy(e => e.Salary)
                    .ThenByDescending(e => e.FirstName)
                    .Select(e => new
                    {
                        Name = e.FirstName + " " + e.LastName,
                        DepartmentName = e.Department.Name,
                        Salary = e.Salary
                    });

                foreach (var r in result)
                {
                    Console.WriteLine($"{r.Name} from {r.DepartmentName} - ${r.Salary:f2}");
                }
            }
        }
    }
}
