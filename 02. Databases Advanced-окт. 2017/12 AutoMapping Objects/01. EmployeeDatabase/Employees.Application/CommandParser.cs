namespace Employees.Application
{
    using Employees.Application.Command;
    using System;
    using System.Linq;
    using System.Reflection;
    using Microsoft.Extensions.DependencyInjection;

    internal class CommandParser
    {
        public static ICommand Parse(IServiceProvider serviceProvider, string commandName)
        {
            var assembly = Assembly.GetExecutingAssembly(); //Take whole project.
            var commandTypes = assembly.GetTypes().Where(t => t.GetInterfaces().Contains(typeof(ICommand))); //find command with interface ICommand.
            var commandType = commandTypes.SingleOrDefault(c => c.Name.ToLower() == $"{commandName.ToLower()}command"); //find current commandName.

            if(commandType == null)
            {
                throw new InvalidOperationException("Invalid command.");
            }

            var constructor = commandType.GetConstructors().First();//find constructor. 
            var constuctorParams = constructor.GetParameters().Select(p => p.ParameterType).ToArray();//get all params for constructor.
            var serviceArgs = constuctorParams.Select(p => serviceProvider.GetService(p)).ToArray();//give params to constructor for current command.

            var command = (ICommand)constructor.Invoke(serviceArgs);

            return command;
        }
    }
}
