namespace PhotoShare.Client.Core.Commands
{
    using System;
    using System.Linq;
    using PhotoShare.Data;

    public class ModifyUserCommand
    {
        // ModifyUser <username> <property> <new value>
        // For example:
        // ModifyUser <username> Password <NewPassword>
        // ModifyUser <username> BornTown <newBornTownName>
        // ModifyUser <username> CurrentTown <newCurrentTownName>
        // !!! Cannot change username
        public static string Execute(string[] data)
        {
            string username = data[1];
            string property = data[2].ToLower();
            string newValue = data[3];

            var loggedUser = Session.User;
            if (loggedUser == null)
            {
                throw new InvalidOperationException("Invalid credentials! You have to login first");
            } 

            using (var db = new PhotoShareContext())
            {
                var user = db.Users
                    .Where(u => u.Username == username)
                    .FirstOrDefault();

                if (user==null)
                {
                    throw new ArgumentException($"User {username} not found! ");
                }
                if(loggedUser.Username != user.Username)
                {
                    throw new InvalidOperationException($"Invalid credentials! {loggedUser.Username} cannot modify {username}'s profile");
                }

                switch (property)
                {
                    case "password":
                        if(!newValue.Any(c=>Char.IsLower(c)) || !newValue.Any(c=>Char.IsDigit(c)))
                        {
                            throw new ArgumentException($"Value {newValue} not valid."
                                + Environment.NewLine + "Invalid Password");
                        }
                        user.Password = newValue;
                        break;
                    case "borntown":
                        var bornTown = db.Towns
                            .Where(t => t.Name == newValue)
                            .FirstOrDefault();

                        if (bornTown == null)
                        {
                            throw new ArgumentException($"Value {newValue} not valid."
                              + Environment.NewLine + $"Town {newValue} not fount");
                        }
                        user.BornTown = bornTown;
                        break;
                    case "currenttown":
                        var currentTown = db.Towns
                            .Where(t => t.Name == newValue)
                            .FirstOrDefault();

                        if (currentTown == null)
                        {
                            throw new ArgumentException($"Value {newValue} not valid."
                              + Environment.NewLine + $"Town {newValue} not fount");
                        }
                        user.CurrentTown = currentTown;
                        break;
                    default:
                        throw new ArgumentException($"Property {property} not supported!");
                }
                db.SaveChanges();
                string result = $"User {username} {property} is {newValue}.";
                return result;
            }
        }
    }
}
