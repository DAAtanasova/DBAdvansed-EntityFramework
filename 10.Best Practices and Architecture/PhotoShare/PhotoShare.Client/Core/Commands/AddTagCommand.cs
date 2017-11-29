namespace PhotoShare.Client.Core.Commands
{
    using Models;
    using Data;
    using Utilities;
    using System.Linq;
    using System;

    public class AddTagCommand
    {
        // AddTag <tag>
        public static string Execute(string[] data)
        {
            string tag = data[1].ValidateOrTransform();

            var loggedUser = Session.User;
            if(loggedUser == null)
            {
                throw new InvalidOperationException("Invalid credentials! You have to login first");
            }
            
            using (PhotoShareContext context = new PhotoShareContext())
            {
                if(context.Tags.Any(t=>t.Name == tag))
                {
                    throw new ArgumentException($"Tag {tag} exists!");
                }

                context.Tags.Add(new Tag
                {
                    Name = tag
                });

                context.SaveChanges();
            }

            return tag + " was added successfully to database!";
        }
    }
}
