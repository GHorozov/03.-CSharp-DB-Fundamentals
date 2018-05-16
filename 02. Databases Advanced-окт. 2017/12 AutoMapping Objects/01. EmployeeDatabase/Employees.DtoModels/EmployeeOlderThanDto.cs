namespace Employees.DtoModels
{ 
    using System;
    using Employees.Models;
    using System.Collections.Generic;

    public class EmployeeOlderThanDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public decimal Salary { get; set; }
        public int? ManagerId { get; set; }
        public DateTime? Birthdate { get; set; }
        public Employee Manager { get; set; }
        public ICollection<Employee> ManagedEmployees { get; set; }
    }
}
