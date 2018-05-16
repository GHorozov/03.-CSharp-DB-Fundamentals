using System;
using System.Linq;
using P02_DatabaseFirst.Data;

namespace P04_SalaryOver50000
{
    class StartUpS
    {
        static void Main(string[] args)
        {
            var db = new SoftUniContext();

            using (db)
            {
                var result= db.Employees
                    .Where(e => e.Salary > 50000)
                    .OrderBy(e => e.FirstName)
                    .Select(e => new
                    {
                        e.FirstName
                    });

                foreach (var e in result)
                {
                    Console.WriteLine(e.FirstName);
                }
            }
        }
    }
}
