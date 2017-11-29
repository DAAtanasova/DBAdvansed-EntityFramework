namespace PhotoShare.Client.Core.Commands
{
    using System;
    using System.Linq;
    using System.Text;
    using Microsoft.EntityFrameworkCore;
    using PhotoShare.Data;

    public class PrintFriendsListCommand 
    {
        // PrintFriendsList <username>
        public static string Execute(string[] data)
        {
            string username = data[1];

            using(var db = new PhotoShareContext())
            {
                var user = db.Users
                    .Include(u=>u.FriendsAdded)
                    .ThenInclude(u=>u.Friend)
                    .SingleOrDefault(u => u.Username == username);
                if(user == null)
                {
                    throw new ArgumentException($"User {username} not found!");
                }

                var userFriends = user.FriendsAdded  
                    .Where(u=>u.Friend.Username!=username)
                    .Select(f => f.Friend.Username)
                    .OrderBy(x=>x)
                    .ToArray();

                string result = "";
                if (userFriends.Length == 0)
                {
                    result =  "No friends for this user. :(";
                }
                else
                {
                    result = "Friends:" + Environment.NewLine + "-"
                        + string.Join(Environment.NewLine + "-", userFriends);
                }
                return result;
            }
        }
    }
}
