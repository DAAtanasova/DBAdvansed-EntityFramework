using System;
using System.Text;
using System.Linq;
using System.Collections.Generic;
using System.Xml.Linq;

using Newtonsoft.Json;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

using Instagraph.Data;
using Instagraph.Models;
using Instagraph.DataProcessor.Dtos;

namespace Instagraph.DataProcessor
{
    public class Deserializer
    {
        private static string errorMsg = "Error: Invalid data.";
        private static string successMsg = "Successfully imported {0}.";

        public static string ImportPictures(InstagraphContext context, string jsonString)
        {
            var allPictures = JsonConvert.DeserializeObject<Picture[]>(jsonString);

            var pictures = new List<Picture>();

            var sb = new StringBuilder();

            foreach (var pic in allPictures)
            {
                bool isValid = !String.IsNullOrWhiteSpace(pic.Path) && pic.Size > 0;
                bool isPicExistinDB = context.Pictures.Any(p => p.Path == pic.Path);
                bool isPicExictInList = pictures.Any(p => p.Path == pic.Path);

                if (!isValid || isPicExistinDB || isPicExictInList)
                {
                    sb.AppendLine(errorMsg);
                }
                else
                {
                    pictures.Add(pic);
                    sb.AppendLine(string.Format(successMsg, $"Picture {pic.Path}"));
                }
            }

            context.Pictures.AddRange(pictures);
            context.SaveChanges();

            var result = sb.ToString().Trim();

            return result;
        }

        public static string ImportUsers(InstagraphContext context, string jsonString)
        {
            var allUsersDto = JsonConvert.DeserializeObject<UserDto[]>(jsonString);

            var sb = new StringBuilder();

            var usersToAdd = new List<User>();

            foreach (var userDto in allUsersDto)
            {
                bool isUsernameValid = !string.IsNullOrWhiteSpace(userDto.Username) && userDto.Username.Length <= 30;

                bool isUserExist = usersToAdd.Any(u => u.Username == userDto.Username);

                bool isPasswordValid = !string.IsNullOrWhiteSpace(userDto.Password) && userDto.Password.Length <= 20;

                bool isPictureValid = !string.IsNullOrWhiteSpace(userDto.ProfilePicture);

                var picture = context.Pictures.FirstOrDefault(p => p.Path == userDto.ProfilePicture);
                
                if (!isUsernameValid || isUserExist || !isPasswordValid || !isPictureValid || picture==null)
                {
                    sb.AppendLine(errorMsg);
                }

                else
                {
                    var user = Mapper.Map<User>(userDto);
                    user.ProfilePicture = picture;
                    
                    usersToAdd.Add(user);
                    sb.AppendLine(string.Format(successMsg, $"User {userDto.Username}"));
                }
            }

            context.Users.AddRange(usersToAdd);
            context.SaveChanges();

            string result = sb.ToString().Trim();
            return result;
        }

        public static string ImportFollowers(InstagraphContext context, string jsonString)
        {
            var userFollowersDto = JsonConvert.DeserializeObject<UserFollowerDto[]>(jsonString);

            var sb = new StringBuilder();

            var usersFollowersToAdd = new List<UserFollower>();

            foreach (var uf in userFollowersDto)
            {
                bool isUserValid = !String.IsNullOrWhiteSpace(uf.User);
                bool isFollowerValid = !String.IsNullOrWhiteSpace(uf.Follower);

                var user = context.Users.FirstOrDefault(u => u.Username == uf.User);
                var follower = context.Users.FirstOrDefault(u => u.Username == uf.Follower);

                if(!isUserValid || !isFollowerValid || user == null || follower == null)
                {
                    sb.AppendLine(errorMsg);
                }

                //isAlreadyFollowing
                else if (usersFollowersToAdd.Any(u => u.UserId == user.Id && u.FollowerId == follower.Id))
                {
                    sb.AppendLine(errorMsg);
                }
                else
                {
                    var userFollowerToAdd = new UserFollower
                    {
                        UserId = user.Id,
                        FollowerId = follower.Id
                    };
                    usersFollowersToAdd.Add(userFollowerToAdd);
                    sb.AppendLine(string.Format(successMsg, $"Follower {follower.Username} to User {user.Username}"));
                }
            }
            context.UsersFollowers.AddRange(usersFollowersToAdd);
            context.SaveChanges();

            string result = sb.ToString().Trim();
            return result;
        }

        public static string ImportPosts(InstagraphContext context, string xmlString)
        {
            var sb = new StringBuilder();
            var postsToAdd = new List<Post>();

            var xPosts = XDocument.Parse(xmlString);

            var elements = xPosts.Root.Elements().ToArray();

            foreach (var el in elements)
            {
                string caption = el.Element("caption")?.Value;
                string username = el.Element("user")?.Value;
                string picturePath = el.Element("picture")?.Value;

                bool IsInputValid = String.IsNullOrWhiteSpace(caption) && !String.IsNullOrWhiteSpace(username) && !String.IsNullOrWhiteSpace(picturePath);

                var user = context.Users.FirstOrDefault(u => u.Username == username);
                var picture = context.Pictures.FirstOrDefault(p => p.Path == picturePath);

                if(IsInputValid || user == null || picture == null)
                {
                    sb.AppendLine(errorMsg);
                }

                else
                {
                    var post = new Post
                    {
                        Caption = caption,
                        UserId = user.Id,
                        PictureId = picture.Id
                    };
                    postsToAdd.Add(post);
                    sb.AppendLine(string.Format(successMsg, $"Post {caption}"));
                }
            }
            context.Posts.AddRange(postsToAdd);
            context.SaveChanges();

            return sb.ToString().TrimEnd(); ;
        }

        public static string ImportComments(InstagraphContext context, string xmlString)
        {
            var sb = new StringBuilder();
            var commentsToAdd = new List<Comment>();

            var xDocComments = XDocument.Parse(xmlString);

            var elements = xDocComments.Root.Elements().ToArray();

            foreach (var el in elements)
            {
                string content = el.Element("content")?.Value;
                string username = el.Element("user")?.Value;
                string postIdString = el.Element("post")?.Attribute("id")?.Value;

                var isInputValid = !string.IsNullOrWhiteSpace(content) &&
                    !string.IsNullOrWhiteSpace(username) && !string.IsNullOrWhiteSpace(postIdString);

                if (!isInputValid)
                {
                    sb.AppendLine(errorMsg);
                    continue;
                }

                var user = context.Users.FirstOrDefault(u => u.Username == username);
                var postId = int.Parse(postIdString);
                var post = context.Posts.Find(postId);

                if(user==null || post==null)
                {
                    sb.AppendLine(errorMsg);
                }
                else
                {
                    var comment = new Comment
                    {
                        Content = content,
                        UserId = user.Id,
                        PostId = postId
                    };
                    commentsToAdd.Add(comment);
                    sb.AppendLine(string.Format(successMsg, $"Comment {content}"));
                }
            }

            context.Comments.AddRange(commentsToAdd);
            context.SaveChanges();

            return sb.ToString().Trim();
        }
    }
}
