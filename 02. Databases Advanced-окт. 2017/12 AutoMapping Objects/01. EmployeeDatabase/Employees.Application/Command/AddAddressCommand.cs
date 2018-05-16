namespace Employees.Application.Command
{
    using System;
    using Employees.Services;
    using System.Linq;

    public class AddAddressCommand : ICommand
    {
        private readonly EmployeesService employeeService;

        public AddAddressCommand(EmployeesService employeeService)
        {
            this.employeeService = employeeService;
        }

        public string Execute(params string[] args)
        {
            var employeeId = int.Parse(args[0]);
            var address = string.Join(" ", args.Skip(1));

            var employeeName = employeeService.SetAddress(employeeId, address);

            return $"{employeeName}'s address is set to {address}.";
        }
    }
}
