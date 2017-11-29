namespace PhotoShare.Client.Core.Commands
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using PhotoShare.Data;
    using PhotoShare.Models;

    public class CreateAlbumCommand
    {
        // CreateAlbum <username> <albumTitle> <BgColor> <tag1> <tag2>...<tagN>
        public static string Execute(string[] data)
        {
            string username = data[1];
            string albumTitle = data[2];
            string color = data[3];
            List<string> allTags = data.Skip(4).ToList();

            var loggedUser = Session.User;
            if (loggedUser == null)
            {
                throw new InvalidOperationException("Invalid credentials! You have to login first");
            }

            using (var db = new PhotoShareContext())
            {
                var userCheck = db.Users
                    .Where(u => u.Username == username)
                    .FirstOrDefault();

                //user doesnt exist
                if (userCheck == null)
                {
                    throw new ArgumentException($"User {username} not found!");
                }

                //if logged user try to create album to another user
                if (loggedUser.Username!=username)
                {
                    throw new InvalidOperationException($"Invalid credentials! {loggedUser.Username}, You cannot create album to {username}'s profile.");
                }

                //album exist
                if (db.Albums.Any(a => a.Name == albumTitle))
                {
                    throw new ArgumentException($"Album {albumTitle} exists!");
                }

                //Is color exist in Enum.Color
                var isColor = Enum.IsDefined(typeof(Color), color);
                if (!isColor)
                {
                    throw new ArgumentException($"Color {color} not found!");
                }
                
                //if any tag is not found
                List<Tag> tags = db.Tags.Where(t => allTags.Contains(t.Name.Substring(1))).ToList();
                if (allTags.Count > tags.Count)
                {
                    throw new ArgumentException("Invalid tags!");
                }
    
                //Create new Album
                var newAlbum = new Album
                {
                    Name = albumTitle,
                    BackgroundColor = Enum.Parse<Color>(color)
                };

                //set loggedUser, who create the Album - an owner
                var albumRole = new AlbumRole
                {
                    User = loggedUser,
                    Album = newAlbum,
                    Role = Role.Owner
                };
                newAlbum.AlbumRoles.Add(albumRole);

                //Create AlbumTags for the new album
                var albumTags = new List<AlbumTag>();
                foreach (var tag in tags)
                {
                    var albumTag = new AlbumTag()
                    {
                        Album = newAlbum,
                        Tag = tag
                    };
                    albumTags.Add(albumTag);
                }

                //Add albumTags with given tags and new album to the DB
                db.AlbumTags.AddRange(albumTags);
                db.SaveChanges();

                string result = $"Album {albumTitle} successfully created!";

                return result;
            }
        }
    }
}
