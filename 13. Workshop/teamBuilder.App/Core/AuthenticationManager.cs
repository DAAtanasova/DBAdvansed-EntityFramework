using System;
using System.Collections.Generic;
using System.Text;
using TeamBuilder.App.Utilities;
using TeamBuilder.Models;

namespace TeamBuilder.App.Core
{
    public class AuthenticationManager
    {
        private static User currentUser;

        public static void Login(User user)
        {
            currentUser = user;
        }

        public static void Logout()
        {
            Autorized();
            currentUser = null;
        }
        
        public static void Autorized()
        {
            if(currentUser == null)
            {
                throw new InvalidOperationException(Constants.ErrorMessages.LoginFirst);
            }
        }

        //isLoged
        public static bool isAuthenticated()
        {
            if(currentUser != null)
            {
                return true;
            }
            return false;
        }

        public static User GetCurrentUser()
        {
            Autorized();
            return currentUser;
        }
    }
}
