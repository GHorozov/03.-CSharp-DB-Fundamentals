namespace Employees.Application.Command
{
    using Employees.Services;
    using System;

    public class SetBirthdayCommand : ICommand
    {
        private readonly EmployeesService employeeService;

        public SetBirthdayCommand(EmployeesService employeeService)
        {
            this.employeeService = employeeService;
        }

        public string Execute(params string[] args)
        {
            var employeeId = int.Parse(args[0]);
            var date = DateTime.ParseExact(args[1], "dd-MM-yyyy", null);

            var employeeName = employeeService.SetBirthday(employeeId, date);

            return $"{employeeName}'s birthday was set to {args[1]}.";
        }
    }
}
