namespace PhotoShare.Client.Core.Commands
{
    using System;
    using System.Linq;
    using Microsoft.EntityFrameworkCore;
    using PhotoShare.Data;
    using PhotoShare.Models;

    public class ShareAlbumCommand
    {
        // ShareAlbum <albumId> <username> <permission>
        // For example:
        // ShareAlbum 4 dragon321 Owner
        // ShareAlbum 4 dragon11 Viewer
        public static string Execute(string[] data)
        {
            var albumId = int.Parse(data[1]);
            var username = data[2];
            var permission = data[3];

            var loggedUser = Session.User;
            if (loggedUser == null)
            {
                throw new InvalidOperationException("Invalid credentials! You have to login first");
            }

            using (var db = new PhotoShareContext())
            {
                var album = db.Albums
                    .Include(a=>a.AlbumRoles)
                    .ThenInclude(a=>a.User)
                    .SingleOrDefault(a => a.Id == albumId);

                if (album == null)
                {
                    throw new ArgumentException($"Album {albumId} not found!");
                }

                var user = db.Users
                    .SingleOrDefault(u => u.Username == username);

                if(user == null)
                {
                    throw new ArgumentException($"User {username} not found!");
                }

                bool isPermission = Enum.IsDefined(typeof(Role), permission);
                if (!isPermission)
                {
                    throw new ArgumentException("Permission must be either “Owner” or “Viewer”!");
                }

                //if someone who doesnt own the album try to share
                bool isLoggedUserOwnTheAlbum = album.AlbumRoles
                    .Any(u => u.User.Username == Session.User.Username && u.Role == Role.Owner);
                if (!isLoggedUserOwnTheAlbum)
                {
                    throw new InvalidOperationException($"Invalid credentials! You cannot share album which you don't own.");
                }
                
                var albumRole = new AlbumRole
                {
                    User = user,
                    Album = album,
                    Role = Enum.Parse<Role>(permission)
                };
                db.AlbumRoles.Add(albumRole);
                db.SaveChanges();
                string result = $"Username {username} added to album {album.Name} ({permission})";
                return result;
            }
        }
    }
}
