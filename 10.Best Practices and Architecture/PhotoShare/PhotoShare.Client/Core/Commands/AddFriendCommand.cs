namespace PhotoShare.Client.Core.Commands
{
    using System;
    using System.Linq;
    using PhotoShare.Data;
    using PhotoShare.Models;
    using Microsoft.EntityFrameworkCore;

    public class AddFriendCommand
    {
        // AddFriend <username1> <username2>
        public static string Execute(string[] data)
        {
            string requestingUsername = data[1];
            string addedFriendUsername = data[2];

            var loggedUser = Session.User;
            if (loggedUser == null)
            {
                throw new InvalidOperationException("Invalid credentials! You have to login first.");
            }

            using (var db = new PhotoShareContext())
            {
                var requestingUser = db.Users
                    .Include(u=>u.FriendsAdded)
                    .FirstOrDefault(u => u.Username == requestingUsername);

                if (requestingUser == null)
                {
                    throw new ArgumentException($"{requestingUsername} not found!");
                }
                
                //if loggedUser try to add friends to another User
                if(loggedUser.Username != requestingUsername)
                {
                    throw new InvalidOperationException($"Invalid credentials! {loggedUser.Username}(loggedUser) cannot add friends to {requestingUsername}'s friendslist");
                }

                var addedUser = db.Users
                    .Include(u => u.FriendsAdded)
                    .FirstOrDefault(u => u.Username == addedFriendUsername);
                if (addedUser == null)
                {
                    throw new ArgumentException($"{addedFriendUsername} not found!");
                }

                bool alreadyAdded = requestingUser.FriendsAdded.Any(u => u.Friend == addedUser);
                bool accepted = addedUser.FriendsAdded.Any(u => u.Friend == requestingUser);
                
                //if they are already friends
                if (alreadyAdded && accepted)
                {
                    throw new InvalidOperationException($"{addedFriendUsername} is already a friend to {requestingUsername}");
                }

                //if user1 already sent request and user2 still not accept it
                if (alreadyAdded && !accepted)
                {
                    throw new ArgumentException("Friend request already sent!");
                }

                requestingUser.FriendsAdded.Add(new Friendship
                {
                    User = requestingUser,
                    Friend = addedUser
                });
                db.SaveChanges();
                string result = $"Friend {addedFriendUsername} added to {requestingUsername}";
                return result;
            }
        }
    }
}
