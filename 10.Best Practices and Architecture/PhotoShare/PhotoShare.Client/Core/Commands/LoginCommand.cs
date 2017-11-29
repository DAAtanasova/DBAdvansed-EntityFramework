using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PhotoShare.Data;

namespace PhotoShare.Client.Core.Commands
{
    public class LoginCommand
    {
        //Login <username> <password>
        public static string Execute(string[] data)
        {
            string username = data[1];
            string password = data[2];
            
            var loggedUser = Session.User;
            if (loggedUser != null)
            {
                throw new ArgumentException("You should logout first!");
            }

            using (var db = new PhotoShareContext())
            {
                var user = db.Users
                    .Where(u => u.Username == username && u.Password == password)
                    .SingleOrDefault();
                if(user == null)
                {
                    throw new ArgumentException($"Invalid username or password!");
                }
                
                Session.User = user;
                string result = $"User {username} successfully logged in!";
                return result;
            }
        }
    }
}
