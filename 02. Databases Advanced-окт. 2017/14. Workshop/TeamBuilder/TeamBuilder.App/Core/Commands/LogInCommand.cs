namespace TeamBuilder.App.Core.Commands
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using TeamBuilder.App.Core.Commands.Interfaces;
    using TeamBuilder.App.Utilities;
    using TeamBuilder.Data;

    public class LogInCommand : ICommand
    {
        //<username> <password>
        public string Execute(IUserSession userSession, params string[] data)
        {
            var username = data[0];
            var password = data[1];

            var context = new TeamBuilderContext();
            var user = context.Users.FirstOrDefault(u => u.Username == username);

            if(user == null)
            {
                throw new ArgumentException(Constants.ErrorMessages.UserOrPasswordIsInvalid);
            }

            if(user.IsDeleted || user.Password != password)
            {
                throw new ArgumentException(Constants.ErrorMessages.UserOrPasswordIsInvalid);
            }

            if (userSession.IsLoggedIn )
            {
                throw new InvalidOperationException(Constants.ErrorMessages.LogoutFirst);
            }

            userSession.LogIn(user);

            return $"User {username} successfully logged in!";
        }
    }
}