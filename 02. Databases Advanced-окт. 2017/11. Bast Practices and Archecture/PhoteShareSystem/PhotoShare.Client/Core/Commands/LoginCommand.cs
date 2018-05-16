namespace PhotoShare.Client.Core.Commands
{
    using System;
    using PhotoShare.Data;
    using System.Linq;

    public class LoginCommand
    {
        //Login <username> <password>
        public static string Execute(params string[] data)
        {
            var username = data[0];
            var password = data[1];

            SecurityService.Login(username, password);

            return $"User {username} successfully logged in!";
        }
    }
}
