namespace Employees.Application.Command
{
    using Employees.Services;
    using System;
    using System.Linq;
    using System.Text;

    public class ListEmployeesOlderThanCommand : ICommand
    {
        private readonly EmployeesService employeeService;

        public ListEmployeesOlderThanCommand(EmployeesService employeeService)
        {
            this.employeeService = employeeService;
        }

        //<age>
        public string Execute(params string[] args)
        {
            var age = int.Parse(args[0]);

            var infoEmployee = employeeService.GetEmployeeOlderThan(age);

            if (infoEmployee == null || infoEmployee.Length == 0)
            {
                throw new ArgumentException($"There are not employees older than {age}");
            }

            var sb = new StringBuilder();

            foreach (var employee in infoEmployee.OrderByDescending(x => x.Salary))
            {
                if (employee.Manager != null)
                {
                    sb.AppendLine($"{employee.FirstName} {employee.LastName} - ${employee.Salary:f2} - Manager: {employee.Manager.LastName}");
                }
                else
                {
                    sb.AppendLine($"{employee.FirstName} {employee.LastName} - ${employee.Salary:f2} - Manager: [no manager]");

                }
            }

            return sb.ToString().Trim() ;
        }
    }
}
