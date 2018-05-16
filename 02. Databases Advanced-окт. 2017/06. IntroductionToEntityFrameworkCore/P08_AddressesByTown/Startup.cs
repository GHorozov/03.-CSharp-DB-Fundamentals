using System;
using System.Linq;
using P02_DatabaseFirst.Data;
using P02_DatabaseFirst.Data.Models;

namespace P08_AddressesByTown
{
    class StartUp
    {
        static void Main(string[] args)
        {
            var db = new SoftUniContext();

            using (db)
            {
                var addresses = db.Addresses
                    .OrderByDescending(x => x.Employees.Count)
                    .ThenBy(t => t.Town.Name)
                    .ThenBy(at => at.AddressText)
                    .Take(10)
                    .Select(a => new
                    {
                        AdressText = a.AddressText,
                        TownName = a.Town.Name,
                        EmpCount = a.Employees.Count
                    });

                foreach (var a in addresses)
                {
                    Console.WriteLine($"{a.AdressText}, {a.TownName} - {a.EmpCount} employees");
                }
            }
        }
    }
}
