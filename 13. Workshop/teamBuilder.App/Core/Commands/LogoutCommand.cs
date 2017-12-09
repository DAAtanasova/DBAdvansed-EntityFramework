using System;
using System.Collections.Generic;
using System.Text;
using TeamBuilder.App.Utilities;

namespace TeamBuilder.App.Core.Commands
{
    public class LogoutCommand
    {
        //[no args]
        public static string Execute(string[] data)
        {
            Check.CheckLenght(0, data);

            var loggedUser = AuthenticationManager.GetCurrentUser();

            AuthenticationManager.Logout();

            return $"User {loggedUser.Username} successfully logged out!";
        }
    }
}
