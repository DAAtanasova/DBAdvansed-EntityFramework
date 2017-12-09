using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using Instagraph.Data;
using Instagraph.DataProcessor.Dtos;
using Newtonsoft.Json;

namespace Instagraph.DataProcessor
{
    public class Serializer
    {
        public static string ExportUncommentedPosts(InstagraphContext context)
        {
            var posts = context.Posts
                .Where(p => p.Comments.Count == 0)
                .OrderBy(p => p.Id)
                .Select(p => new
                {
                    Id = p.Id,
                    Picture = p.Picture.Path,
                    User = p.User.Username
                });

            var resultPosts = JsonConvert.SerializeObject(posts, Formatting.Indented);

            return resultPosts;
        }

        public static string ExportPopularUsers(InstagraphContext context)
        {
            var users = context.Users  //brykni v userite
                .Where(u=>u.Posts //ot tehnite postove
                    .Any(p=>p.Comments // vzemi komentarite
                        .Any(c=>u.Followers //chiito user - avtor
                            .Any(f=>f.FollowerId == c.UserId)))) // e nqkoi ot posledovatelite
                 .OrderBy(u=>u.Id)
                 .Select(x=> new
                 {
                     User = x.Username,
                     Followers = x.Followers.Count
                 });

            var result = JsonConvert.SerializeObject(users, Formatting.Indented);
            return result;
        }

        public static string ExportCommentsOnPosts(InstagraphContext context)
        {
            var allusers = context.Users
                .Select(u => new
                {
                    Username = u.Username,
                    PostsCommentsCounts = u.Posts.Select(p => p.Comments.Count).ToArray()
                });

            var xDocUsers = new XDocument();
            xDocUsers.Add(new XElement("users"));
            var usersCommentsCountDto = new List<UserCommentsCountDto>();

            foreach (var user in allusers)
            {
                string username = user.Username;
                var maxCommentsCount = user.PostsCommentsCounts.Length == 0 ? 0 : user.PostsCommentsCounts.Max();

                var userComm = new UserCommentsCountDto
                {
                    Username = username,
                    MaxCommentsCount = maxCommentsCount
                };

                usersCommentsCountDto.Add(userComm);
            }

            foreach (var u in usersCommentsCountDto.OrderByDescending(c=>c.MaxCommentsCount).ThenBy(u=>u.Username))
            {
                xDocUsers.Element("users")
                         .Add(new XElement("user",
                         new XElement("Username", u.Username),
                         new XElement("MostComments", u.MaxCommentsCount)));
            }

            string result = xDocUsers.ToString();
            return result;
        }
    }
}
