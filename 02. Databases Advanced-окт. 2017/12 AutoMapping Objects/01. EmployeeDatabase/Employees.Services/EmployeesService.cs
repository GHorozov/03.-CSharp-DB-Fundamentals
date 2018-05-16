namespace Employees.Services
{
    using System;
    using AutoMapper;
    using Employees.Data;
    using Employees.Models;
    using Employees.DtoModels;
    using AutoMapper.QueryableExtensions;
    using System.Linq;

    public class EmployeesService
    {
        private readonly EmployeesContext context;

        public EmployeesService(EmployeesContext context)
        {
            this.context = context;
        }

        public EmployeeDto ById(int employeeId)
        {
            var employee = context.Employees.Find(employeeId); //take employee with employeeId from database
            var employeeDto = Mapper.Map<EmployeeDto>(employee); //map employee from database to EmployeeDto

            return employeeDto;
        }

        public void AddEmployee(EmployeeDto dto)
        {
            var employee = Mapper.Map<Employee>(dto);
            context.Employees.Add(employee);

            context.SaveChanges();
        }

        public string SetBirthday(int employeeId, DateTime date)
        {
            var employee = context.Employees.Find(employeeId);
            employee.Birthdate = date;

            context.SaveChanges();

            return $"{employee.FirstName} {employee.LastName}";
        }

        public string SetAddress(int employeeId, string address)
        {
            var employee = context.Employees.Find(employeeId);
            employee.Address = address;

            context.SaveChanges();

            return $"{employee.FirstName} {employee.LastName}";
        }

        public string GetEmployeeInfo(int employeeId)
        {
            var employee = context.Employees.Find(employeeId);

            return $"{employee.Id} {employee.FirstName} {employee.LastName} {employee.Salary:f2}";
        }

        public EmployeePersonalDto PersonalById(int employeeId)
        {
            var employee = context.Employees.Find(employeeId);
            var employeePersonalDto = Mapper.Map<EmployeePersonalDto>(employee);

            return employeePersonalDto;
        }

        public string SetManager(int employeeId, int managerId)
        {
            var employee = context.Employees.Find(employeeId);
            var manager = context.Employees.Find(managerId);

            employee.Manager = manager;
            context.SaveChanges();

            return $"{manager.FirstName} {manager.LastName} was set as manager to {employee.FirstName} {employee.LastName}";
        }

        public ManagerDto GetManagerInfo(int employeeId)
        {
            var employee = context.Employees.Find(employeeId);
            var managerDto = Mapper.Map<ManagerDto>(employee);

            return managerDto;
        }

        public EmployeeOlderThanDto[] GetEmployeeOlderThan(int age)
        {
            var timeNow = DateTime.Now;
            var employee = context.Employees.Where(e => (timeNow.Year - e.Birthdate.Value.Year) >= age)
                            .ProjectTo<EmployeeOlderThanDto>()
                            .ToArray();

            return employee;
        }
    }
}
