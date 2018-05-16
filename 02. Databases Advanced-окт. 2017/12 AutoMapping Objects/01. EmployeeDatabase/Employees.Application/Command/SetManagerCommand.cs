namespace Employees.Application.Command
{
    using Employees.Services;

    public class SetManagerCommand :ICommand
    {
        private readonly EmployeesService employeeService;

        public SetManagerCommand(EmployeesService employeeService)
        {
            this.employeeService = employeeService;
        }

        //<employeeId> <managerId> 
        public string Execute(params string[] args)
        {
            var employeeId = int.Parse(args[0]);
            var managerId = int.Parse(args[1]);

            var manager = employeeService.SetManager(employeeId, managerId);

            return manager;
        }
    }
}
