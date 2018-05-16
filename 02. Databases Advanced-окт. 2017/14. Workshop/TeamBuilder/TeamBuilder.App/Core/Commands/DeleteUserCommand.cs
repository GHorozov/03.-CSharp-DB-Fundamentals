namespace TeamBuilder.App.Core.Commands
{
    using System;
    using System.Linq;
    using TeamBuilder.App.Core.Commands.Interfaces;
    using TeamBuilder.App.Utilities;
    using TeamBuilder.Data;
    using TeamBuilder.Models;

    public class DeleteUserCommand : ICommand
    {
        public string Execute(IUserSession userSession, params string[] data)
        {
            User user = userSession.User;

            if (user == null)
            {
                throw new InvalidOperationException(Constants.ErrorMessages.LoginFirst);
            }

            var context = new TeamBuilderContext();

            var userFromDatabase = context.Users.FirstOrDefault(u => u.UserId == user.UserId);
            userFromDatabase.IsDeleted = true;
            context.SaveChanges();

            userSession.LogOut();

            return $"User {user.Username} was deleted successfully!";
        }
    }
}