namespace Employees.Application.Command
{
    using System;
    using Employees.Services;

    class EmployeePersonalInfoCommand : ICommand
    {
        private readonly EmployeesService employeeService;

        public EmployeePersonalInfoCommand(EmployeesService employeeService)
        {
            this.employeeService = employeeService;
        }

        public string Execute(params string[] args)
        {
            var employeeId = int.Parse(args[0]);

            var e = employeeService.PersonalById(employeeId);

            var birthday = "[no birthday specified]";
            if(e.Birthdate != null)
            {
                birthday = e.Birthdate.Value.ToString("dd-MM-yyyy");
            }

            var address = e.Address ?? "[no address specified]";

            string result = $"ID: {e.Id} - {e.FirstName} {e.LastName} - ${e.Salary:f2}" + Environment.NewLine +
                            $"Birthday: {birthday}" + Environment.NewLine +
                            $"Address: {address}";

            return result;
        }
    }
}
