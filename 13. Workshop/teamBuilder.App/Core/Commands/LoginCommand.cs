using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TeamBuilder.App.Utilities;
using TeamBuilder.Data;

namespace TeamBuilder.App.Core.Commands
{
    
    public class LoginCommand
    {
        //<username> <password>
        public static string Execute(string[] data)
        {
            Check.CheckLenght(2, data);

            if (AuthenticationManager.isAuthenticated())
            {
                throw new InvalidOperationException(Constants.ErrorMessages.LogoutFirst);
            }

            string username = data[0];
            string password = data[1];
            using(var db = new TeamBuilderContext())
            {
                var user = db.Users.SingleOrDefault(u => u.Username == username);
                if (user == null || user.Password != password || user.IsDeleted == true)
                {
                    throw new ArgumentException(Constants.ErrorMessages.UserOrPasswordIsInvalid);
                }

                AuthenticationManager.Login(user);
            }

            return $"User {username} successfully logged in!";
        }
    }
}
