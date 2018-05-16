namespace Employees.Application.Command
{
    using System;
    using System.Text;
    using Employees.Services;

    class ManagerInfoCommand : ICommand
    {
        private readonly EmployeesService employeeService;

        public ManagerInfoCommand(EmployeesService employeeService)
        {
            this.employeeService = employeeService;
        }

        //<employeeId> 
        public string Execute(params string[] args)
        {
            var employeeId = int.Parse(args[0]);

            var managerDto = employeeService.GetManagerInfo(employeeId);

            var sb = new StringBuilder();

            sb.AppendLine($"{managerDto.FirstName} {managerDto.LastName} | Employees: {managerDto.ManagedEmployees.Count}");

            foreach (var employee in managerDto.ManagedEmployees)
            {
                sb.AppendLine($"  -{employee.FirstName} {employee.LastName} - ${employee.Salary:f2}");
            }

            return sb.ToString().Trim();
        }
    }
}
