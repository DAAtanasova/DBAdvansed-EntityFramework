using System;
using System.Collections.Generic;
using System.Text;

namespace PhotoShare.Client.Core.Commands
{
    public class LogoutCommand
    {
        //Logout
        public static string Execute()
        {
            if (Session.User == null)
            {
                throw new InvalidOperationException("You should log in first in order to logout.");
            }

            var loggedUser = Session.User.Username;
            Session.User = null;
            string result = $"User {loggedUser} successfully logged out!";
            return result;
        }
    }
}
