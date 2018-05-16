namespace Employees.Application.Command
{
    internal interface ICommand
    {
        string Execute(params string[] args);
    }
}
