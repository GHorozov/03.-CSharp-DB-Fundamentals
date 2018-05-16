namespace TeamBuilder.App.Core.Commands
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using TeamBuilder.App.Core.Commands.Interfaces;
    using TeamBuilder.App.Utilities;
    using TeamBuilder.Data;
    using TeamBuilder.Models;

    public class RegisterUserCommand : ICommand
    {
        public IUserSession userSession;

        //<username> <password> <repeat-password> <firstName> <lastName> <age> <gender>
        public string Execute(IUserSession userSession, params string[] data)
        {
            var username = data[0];
            var password = data[1];
            var repeatPassword = data[2];
            var firstName = data[3];
            var lastName = data[4];
            var age = int.Parse(data[5]);

            var context = new TeamBuilderContext();

            if (username.Length < Constants.MinUsernameLength || username.Length > Constants.MaxUsernameLength)
            {
                throw new ArgumentException(string.Format(Constants.ErrorMessages.UsernameNotValid, username));
            }

            var user = context.Users.FirstOrDefault(u => u.Username == username);

            if (user != null)
            {
                throw new InvalidOperationException(string.Format(Constants.ErrorMessages.UsernameIsTaken, username));
            }

            if (!Check.isPasswordValid(password))
            {
                throw new ArgumentException(string.Format(Constants.ErrorMessages.PasswordNotValid, password));
            }

            if (password != repeatPassword)
            {
                throw new InvalidOperationException(Constants.ErrorMessages.PasswordDoesNotMatch);
            }

            if (age < 0 || age > int.MaxValue)
            {
                throw new ArgumentException(Constants.ErrorMessages.AgeNotValid);
            }

            Gender gender;
            var isGenderValid = Enum.TryParse(data[6], out gender);
            if (!isGenderValid)
            {
                throw new ArgumentException(Constants.ErrorMessages.GenderNotValid);
            }


            if (userSession.IsLoggedIn)
            {
                throw new InvalidOperationException(Constants.ErrorMessages.LogoutFirst);
            }

            var newUser = new User()
            {
                Username = username,
                Password = password,
                FirstName = firstName,
                LastName = lastName,
                Age = age,
                Gender = gender
            };

            context.Users.Add(newUser);
            context.SaveChanges();

            return $"User {username} was registered successfully!";
        }
    }
}