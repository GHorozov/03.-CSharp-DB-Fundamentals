namespace TeamBuilder.App.Core
{
    using System;
    using System.Linq;
    using System.Reflection;
    using TeamBuilder.App.Core.Commands.Interfaces;

    public class CommandDispatcher
    {
        internal static ICommand Dispatch(string commandName, string[] commandArgs)
        {
            var assembly = Assembly.GetExecutingAssembly();
            var commandWithInterfaceType = assembly.GetTypes().Where(t => t.GetInterfaces().Contains(typeof(ICommand)));
            var commandType = commandWithInterfaceType.SingleOrDefault(c => c.Name.ToLower() == $"{commandName.ToLower()}command");

            if(commandType == null)
            {
                throw new InvalidOperationException("Invalid command.");
            }

            var constructor = commandType.GetConstructors().First();
            var constructorParams = constructor.GetParameters().Select(p => p.ParameterType).ToArray();
            var commandArguments = constructorParams.Select(p => commandArgs.GetType()).ToArray();

            var command = (ICommand)constructor.Invoke(commandArguments);

            return command;
        }
    }
}