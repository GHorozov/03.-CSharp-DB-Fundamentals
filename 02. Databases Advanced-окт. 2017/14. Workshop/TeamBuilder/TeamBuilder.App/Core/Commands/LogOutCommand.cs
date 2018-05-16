namespace TeamBuilder.App.Core.Commands
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using TeamBuilder.App.Core.Commands.Interfaces;
    using TeamBuilder.App.Utilities;
    using TeamBuilder.Models;

    public class LogOutCommand : ICommand
    {
        public string Execute(IUserSession userSession, params string[] data)
        {
            User user = userSession.User;

            if (user == null)
            {
                throw new InvalidOperationException(Constants.ErrorMessages.LoginFirst);
            }

            userSession.LogOut();

            return $"User {user.Username} successfully logged out!";
        }
    }
}