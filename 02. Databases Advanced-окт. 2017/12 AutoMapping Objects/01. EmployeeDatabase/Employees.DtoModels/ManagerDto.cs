namespace Employees.DtoModels
{
    using System;
    using Employees.Models;
    using System.Collections.Generic;
    using System.Text;

    public class ManagerDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public ICollection<Employee> ManagedEmployees { get; set; }

    }
}
