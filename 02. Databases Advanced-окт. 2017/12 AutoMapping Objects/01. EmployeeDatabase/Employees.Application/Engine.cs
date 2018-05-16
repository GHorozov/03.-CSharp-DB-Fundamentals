namespace Employees.Application
{
    using System;
    using System.Linq;

    public class Engine
    {
        private readonly IServiceProvider serviceProvider;

        public Engine(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }

        public void Run()
        {
            while (true)
            {
                try
                {
                    string input = Console.ReadLine();
                    string[] commandParts = input.Split(' ').ToArray();
                    string commandName = commandParts[0];
                    string[] commandArgs = commandParts.Skip(1).ToArray();
                    var command = CommandParser.Parse(serviceProvider, commandName);
                    var result = command.Execute(commandArgs);

                    Console.WriteLine(result);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }
    }
}
