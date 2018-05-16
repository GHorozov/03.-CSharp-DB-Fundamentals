namespace Employees.Application.Command
{
    using Employees.DtoModels;
    using Employees.Services;

    public class AddEmployeeCommand : ICommand
    {
        private readonly EmployeesService employeeService;

        public AddEmployeeCommand(EmployeesService employeeService)
        {
            this.employeeService = employeeService;
        }

        public string Execute(params string[] args)
        {
            var firstName = args[0];
            var lastName = args[1];
            var salary = decimal.Parse(args[2]);

            var employeeDto = new EmployeeDto(firstName, lastName, salary);
            employeeService.AddEmployee(employeeDto);

            return $"Employee {firstName} {lastName} successfully added.";
        }
    }
}
