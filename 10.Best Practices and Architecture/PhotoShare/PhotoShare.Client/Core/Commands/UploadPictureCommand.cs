namespace PhotoShare.Client.Core.Commands
{
    using System;
    using System.Linq;
    using Microsoft.EntityFrameworkCore;
    using PhotoShare.Data;
    using PhotoShare.Models;

    public class UploadPictureCommand
    {
        // UploadPicture <albumName> <pictureTitle> <pictureFilePath>
        public static string Execute(string[] data)
        {
            string albumName = data[1];
            string pictureTitle = data[2];
            string filePath = data[3];

            var loggedUser = Session.User;
            if (loggedUser == null)
            {
                throw new InvalidOperationException("Invalid credentials! You have to login first");
            }

            using (var db = new PhotoShareContext())
            {
                var album = db.Albums
                    .Include(a=>a.AlbumRoles)
                    .ThenInclude(u=>u.User)
                    .SingleOrDefault(a => a.Name == albumName);
                if (album == null)
                {
                    throw new ArgumentException($"Album {albumName} not found!");
                }

                //logged user who doesnt own the album cannot add picture to it
                bool isLoggedUserOwnTheAlbum = album.AlbumRoles
                    .Any(u => u.User.Username == loggedUser.Username && u.Role == Role.Owner);
                if (!isLoggedUserOwnTheAlbum)
                {
                    throw new InvalidOperationException($"Invalid credentials! You cannot add picture to album you don't own.");
                }

                var picture = new Picture
                {
                    Title = pictureTitle,
                    Path = filePath,
                    Album = album
                };
                db.Pictures.Add(picture);
                db.SaveChanges();
                string result = $"Picture {pictureTitle} added to {albumName}!";
                return result;
            }
        }
    }
}
