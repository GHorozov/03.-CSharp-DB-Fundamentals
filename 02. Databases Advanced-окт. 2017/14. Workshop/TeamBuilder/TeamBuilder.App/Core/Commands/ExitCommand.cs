namespace TeamBuilder.App.Core.Commands
{
    using System;
    using TeamBuilder.App.Core.Commands.Interfaces;

    public class ExitCommand : ICommand
    {
        public string Execute(IUserSession userSession, params string[] args)
        {
            Environment.Exit(0);
            return null;
        }
    }
}