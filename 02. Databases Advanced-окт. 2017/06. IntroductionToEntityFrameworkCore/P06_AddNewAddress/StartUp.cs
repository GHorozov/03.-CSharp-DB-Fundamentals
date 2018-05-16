using System;
using System.Linq;
using P02_DatabaseFirst.Data;
using P02_DatabaseFirst.Data.Models;

namespace P06_AddNewAddress
{
    class StartUp
    {
        static void Main(string[] args)
        {
            var db = new SoftUniContext();

            using (db)
            {
                var address = new Address
                {
                    AddressText = "Vitoshka 15",
                    TownId = 4
                };

                Employee employeeNakov = db.Employees.Where(x => x.LastName == "Nakov").SingleOrDefault();
                db.Addresses.Add(address);

                employeeNakov.Address = address; //save new added address to nakov in employee table
                db.SaveChanges();

                var result = db.Employees
                    .OrderByDescending(e => e.AddressId)
                    .Take(10)
                    .Select(e => e.Address.AddressText);

                foreach (var r in result)
                {
                    Console.WriteLine(r);
                }
            }
        }
    }
}
