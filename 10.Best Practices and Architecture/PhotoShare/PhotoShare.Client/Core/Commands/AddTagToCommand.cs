namespace PhotoShare.Client.Core.Commands
{
    using System;
    using System.Linq;
    using Microsoft.EntityFrameworkCore;
    using PhotoShare.Data;
    using PhotoShare.Models;

    public class AddTagToCommand 
    {
        // AddTagTo <albumName> <tag>
        public static string Execute(string[] data)
        {
            string albumName = data[1];
            string tagName = data[2];

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
                    .Where(a => a.Name == albumName)
                    .FirstOrDefault();
                if (album == null)
                {
                    throw new ArgumentException("Either tag or album do not exist!");
                }

                var tag = db.Tags
                    .Where(t => t.Name == "#"+tagName)
                    .FirstOrDefault();
                if(tag == null)
                {
                    throw new ArgumentException("Either tag or album do not exist!");
                }

                //if loggedUser try to add tag to album he doesnt own
                bool isLoggedUserOwnTheAlbum = album.AlbumRoles
                   .Any(u => u.User.Username == loggedUser.Username && u.Role == Role.Owner);
                if (!isLoggedUserOwnTheAlbum)
                {
                    throw new InvalidOperationException($"Invalid credentials! You cannot add tag to album you don't own.");
                }

                var albumTag = new AlbumTag
                {
                    Album = album,
                    Tag = tag
                };
                db.AlbumTags.Add(albumTag);
                db.SaveChanges();

                string result = $"Tag {tagName} added to {albumName}!";
                return result;
            }
        }
    }
}
