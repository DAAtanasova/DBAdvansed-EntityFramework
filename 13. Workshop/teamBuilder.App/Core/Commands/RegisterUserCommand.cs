using System;
using System.Collections.Generic;
using System.Text;
using TeamBuilder.App.Utilities;
using TeamBuilder.Data;
using TeamBuilder.Models;

namespace TeamBuilder.App.Core.Commands
{
    public class RegisterUserCommand
    {
        //<username> <password> <repeat-password> <firstName> <lastName> <age> <gender>
        public static string Execute(string[] data)
        {
            Check.CheckLenght(7, data);

            //username checks
            string username = data[0];
            if(username.Length < Constants.MinUsernameLength || username.Length > Constants.MaxUsernameLength)
            {
                throw new ArgumentException(Constants.ErrorMessages.UsernameNotValid,username);
            }
            if (CommandHelper.IsUserExisting(username))
            {
                string error = string.Format(Constants.ErrorMessages.UsernameIsTaken, username);
                throw new InvalidOperationException(error);
            }

            //password checks
            string password = data[1];
            if (password.Length < Constants.MinPasswordLength || password.Length > Constants.MaxPasswordLength)
            {
                string error = string.Format(Constants.ErrorMessages.PasswordNotValid, password);
                throw new ArgumentException(error);
            }
            string repetedPassword = data[2];
            if(password != repetedPassword)
            {
                throw new InvalidOperationException(Constants.ErrorMessages.PasswordDoesNotMatch);
            }

            //names checks
            string firstName = data[3];
            string lastName = data[4];
            if(firstName.Length > Constants.MaxFirstNameLength || lastName.Length > Constants.MaxLastNameLength)
            {
                throw new ArgumentException("Invalid FirstName or LastName");
            }

            //age checks
            int age;
            var isNumber = int.TryParse(data[5], out age); 
            if(!isNumber || age < 0)
            {
                throw new ArgumentException(Constants.ErrorMessages.AgeNotValid);
            }

            //gender checks
            var gender = data[6];
            bool isGender = Enum.IsDefined(typeof(Gender), gender);
            if (!isGender)
            {
                throw new ArgumentException(Constants.ErrorMessages.GenderNotValid);
            }

            //is there login user
            if (AuthenticationManager.isAuthenticated())
            {
                throw new InvalidOperationException(Constants.ErrorMessages.LogoutFirst);
            }

            var user = new User
            {
                Username = username,
                FirstName = firstName,
                LastName = lastName,
                Password = password,
                Gender = Enum.Parse<Gender>(gender),
                Age = age
            };

            using(var db = new TeamBuilderContext())
            {
                db.Users.Add(user);
                db.SaveChanges();
            }

            return $"User {username} was registered successfully!";

        }
    }
}
