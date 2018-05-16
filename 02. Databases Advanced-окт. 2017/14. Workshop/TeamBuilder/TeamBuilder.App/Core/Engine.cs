namespace TeamBuilder.App.Core
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using TeamBuilder.App.Core.Commands.Interfaces;

    public class Engine
    {
        private CommandDispatcher commandDispatcher;
        private IUserSession userSession;

        public Engine(CommandDispatcher commandDispatcher)
        {
            this.commandDispatcher = commandDispatcher;
            userSession = new UserSession();
        }

        public void Run()
        {
            while (true)
            {
                try
                {
                    var input = Console.ReadLine();
                    var splittedInput = input.Split(new string[] { " ", "\t"}, StringSplitOptions.RemoveEmptyEntries);
                    var commandName = splittedInput[0];
                    var commandArgs = splittedInput.Skip(1).ToArray();
                    var command = CommandDispatcher.Dispatch(commandName, commandArgs);
                    var result = command.Execute(userSession, commandArgs);

                    Console.WriteLine(result);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.GetBaseException().Message);
                }
            }
        }
    }
}