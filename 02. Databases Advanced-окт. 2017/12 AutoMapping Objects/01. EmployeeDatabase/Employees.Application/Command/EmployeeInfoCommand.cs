namespace Employees.Application.Command
{
    using System;
    using Employees.Services;
    
    public class EmployeeInfoCommand : ICommand
    {
        private readonly EmployeesService employeeService;

        public EmployeeInfoCommand(EmployeesService employeeService)
        {
            this.employeeService = employeeService;
        }

        public string Execute(params string[] args)
        {
            var employeeId = int.Parse(args[0]);
            var info = employeeService.GetEmployeeInfo(employeeId);

            return info;
        }
    }
}
