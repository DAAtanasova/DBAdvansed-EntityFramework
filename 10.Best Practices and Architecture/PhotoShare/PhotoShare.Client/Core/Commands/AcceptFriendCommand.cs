namespace PhotoShare.Client.Core.Commands
{
    using System;
    using System.Linq;
    using Microsoft.EntityFrameworkCore;
    using PhotoShare.Data;
    using PhotoShare.Models;

    public class AcceptFriendCommand
    {
        // AcceptFriend <username1> <username2>
        public static string Execute(string[] data)
        {
            string addedUsername = data[1];
            string requestedUsername = data[2];

            var loggedUser = Session.User;
            if (loggedUser == null)
            {
                throw new InvalidOperationException("Invalid credentials! You have to login first.");
            }

            using (var db = new PhotoShareContext())
            {
                var addedUser = db.Users
                    .Include(u=>u.FriendsAdded)
                    .Where(u => u.Username == addedUsername)
                    .FirstOrDefault();

                if(addedUser == null)
                {
                    throw new ArgumentException($"{addedUsername} not found!");
                }

                var requestedUser = db.Users
                    .Include(u=>u.FriendsAdded)
                    .Where(u => u.Username == requestedUsername)
                    .FirstOrDefault();

                if (requestedUser == null)
                {
                    throw new ArgumentException($"{requestedUsername} not found!");
                }

                //if User1 has User2 in FriendsAdded List and User2 has User1 in FriendsAdded List = friends
                bool alreadyAdded = requestedUser.FriendsAdded.Any(u => u.Friend == addedUser);
                bool accepted = addedUser.FriendsAdded.Any(u => u.Friend == requestedUser);

                //if they are friends
                if (alreadyAdded && accepted)
                {
                    throw new InvalidOperationException($"{requestedUsername} is already a friend to {addedUsername}");
                }
                
                //if there is no such request
                if (!alreadyAdded)
                {
                    throw new InvalidOperationException($"{requestedUsername} has not added {addedUsername} as a friend");
                }

                addedUser.FriendsAdded.Add(new Friendship
                {
                    User = addedUser,
                    Friend = requestedUser
                });
                db.SaveChanges();
                string result = $"{addedUsername} accepted {requestedUsername} as a friend";
                return result;
            }
        }
    }
}
