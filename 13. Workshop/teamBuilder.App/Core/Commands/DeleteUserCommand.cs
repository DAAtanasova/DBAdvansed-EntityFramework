using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TeamBuilder.App.Utilities;
using TeamBuilder.Data;

namespace TeamBuilder.App.Core.Commands
{
    public class DeleteUserCommand
    {
        //[no args]
        public static string Execute(string[] data)
        {
            Check.CheckLenght(0, data);

            //if there is no logged user = exception
            AuthenticationManager.Autorized();

            var currentUser = AuthenticationManager.GetCurrentUser();

            using(var db = new TeamBuilderContext())
            {
                var user = db.Users.SingleOrDefault(c => c.Username == currentUser.Username);

                user.IsDeleted = true;
                db.SaveChanges();

                AuthenticationManager.Logout();
            }

            return $"User {currentUser.Username} was deleted successfully!";
        }
    }
}
