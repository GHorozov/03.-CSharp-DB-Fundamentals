namespace Employees.Application.Command
{
    using System;

    public class ExitCommand : ICommand
    {
        public string Execute(params string[] args)
        {
            Console.WriteLine("Goodbye!");
            Environment.Exit(0);

            return String.Empty; 
        }
    }
}
