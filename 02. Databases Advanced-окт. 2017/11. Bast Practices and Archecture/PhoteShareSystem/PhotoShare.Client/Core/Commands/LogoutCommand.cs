namespace PhotoShare.Client.Core.Commands
{
    using PhotoShare.Models;
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class LogoutCommand
    {
        public static string Execute(params string[] data)
        {
            User user = SecurityService.GetCurrentUser();

            if (user == null)
            {
                throw new InvalidOperationException("You should log in first in order to logout.");
            }

            SecurityService.Logout();

            return $"User {user.Username} successfully logged out!";
        }
    }
}
