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
using Instagraph.DataProcessor.Dto;

namespace Instagraph.DataProcessor
{
    public class Deserializer
    {
        private static string successMsg = "Successfully imported {0}.";
        private static string errorMsg = "Error: Invalid data.";

        public static string ImportPictures(InstagraphContext context, string jsonString)
        {
            var deserializedPictures = JsonConvert.DeserializeObject<Picture[]>(jsonString);

            var pictures= new List<Picture>();
            var sb = new StringBuilder();

            foreach (var picture in deserializedPictures)
            {
                var isValid = !String.IsNullOrWhiteSpace(picture.Path) && picture.Size > 0;

                var pictureExist = context.Pictures.Any(pic => pic.Path == picture.Path) || 
                    pictures.Any(pic => pic.Path == picture.Path);

                if (pictureExist || !isValid)
                {
                    sb.AppendLine(errorMsg);
                    continue;
                }

                pictures.Add(picture);
                sb.AppendLine(String.Format(successMsg, $"Picture {picture.Path}"));
            }

            context.Pictures.AddRange(pictures);
            context.SaveChanges();

            var result = sb.ToString().Trim();
            return result;
        }

        public static string ImportUsers(InstagraphContext context, string jsonString)
        {
            var deserializedUsers = JsonConvert.DeserializeObject<UserDto[]>(jsonString);

            var users = new List<User>();
            var sb = new StringBuilder();

            foreach (var u in deserializedUsers)
            {
                var isValidProfilePicture = context.Pictures.Any(p => p.Path == u.ProfilePicture);
                

                var isValidUsername = !String.IsNullOrWhiteSpace(u.Username) && u.Username.Length <= 30;
                var isValidPassword = !String.IsNullOrWhiteSpace(u.Password) && u.Password.Length <= 20;

                if(!isValidProfilePicture || !isValidUsername || !isValidPassword)
                {
                    sb.AppendLine(errorMsg);
                    continue;
                }

                var profilePictureId = context.Pictures.FirstOrDefault(p => p.Path == u.ProfilePicture).Id;

                var newUser = new User()
                {
                    ProfilePictureId = profilePictureId,
                    Username= u.Username,
                    Password= u.Password
                };

                users.Add(newUser);
                sb.AppendLine(String.Format(successMsg, $"User {u.Username}"));
            }

            context.Users.AddRange(users);
            context.SaveChanges();

            var result = sb.ToString().Trim();
            return result;
        }

        public static string ImportFollowers(InstagraphContext context, string jsonString)
        {
            var deserializedFollowers = JsonConvert.DeserializeObject<UserFollowerDto[]>(jsonString);

            var sb = new StringBuilder();

            var followers = new List<UserFollower>();

            foreach (var dto in deserializedFollowers)
            {
                int? userId = context.Users.FirstOrDefault(u => u.Username == dto.User)?.Id;
                int? followerId = context.Users.FirstOrDefault(u => u.Username == dto.Follower)?.Id;

                if (userId == null || followerId == null)
                {
                    sb.AppendLine(errorMsg);
                    continue;
                }

                bool alreadyFollowed = followers.Any(f => f.UserId == userId && f.FollowerId == followerId);
                if (alreadyFollowed)
                {
                    sb.AppendLine(errorMsg);
                    continue;
                }

                var userFollower = new UserFollower()
                {
                    UserId = userId.Value,
                    FollowerId = followerId.Value
                };

                followers.Add(userFollower);
                sb.AppendLine(String.Format(successMsg, $"Follower {dto.Follower} to User {dto.User}"));
            }

            context.UsersFollowers.AddRange(followers);
            context.SaveChanges();

            string result = sb.ToString().TrimEnd();
            return result;
        }

        public static string ImportPosts(InstagraphContext context, string xmlString)
        {
            var xDoc = XDocument.Parse(xmlString);
            var elements = xDoc.Root.Elements();

            var sb = new StringBuilder();
            var postsList = new List<Post>();
            foreach (var e in elements)
            {
                var caption = e.Element("caption")?.Value;
                var user = e.Element("user")?.Value;
                var picture = e.Element("picture")?.Value;

                var isValid = !String.IsNullOrWhiteSpace(caption) ||
                              !String.IsNullOrWhiteSpace(user) ||
                              !String.IsNullOrWhiteSpace(picture);

                var userId = context.Users.FirstOrDefault(u => u.Username == user)?.Id;
                var currentPictureId = context.Pictures.FirstOrDefault(p => p.Path == picture)?.Id;

                if(!isValid || userId == null || currentPictureId == null)
                {
                    sb.AppendLine(errorMsg);
                    continue;
                }

                var newPost = new Post()
                {
                    Caption = caption,
                    UserId = userId.Value,
                    PictureId = currentPictureId.Value
                };

                postsList.Add(newPost);
                sb.AppendLine(String.Format(successMsg, $"Post {caption}"));
            }

            context.Posts.AddRange(postsList);
            context.SaveChanges();

            var result = sb.ToString().Trim();
            return result;
        }

        public static string ImportComments(InstagraphContext context, string xmlString)
        {
            var xDoc = XDocument.Parse(xmlString);
            var elements = xDoc.Root.Elements();

            var sb = new StringBuilder();

            var comments = new List<Comment>();

            foreach (var element in elements)
            {
                string content = element.Element("content")?.Value;
                string userName = element.Element("user")?.Value;
                string postIdString = element.Element("post")?.Attribute("id")?.Value;

                bool noNullInput = !String.IsNullOrWhiteSpace(content) &&
                    !String.IsNullOrWhiteSpace(userName) &&
                    !String.IsNullOrWhiteSpace(postIdString);

                if (!noNullInput)
                {
                    sb.AppendLine(errorMsg);
                    continue;
                }

                int postId = int.Parse(postIdString);

                int? userId = context.Users.FirstOrDefault(u => u.Username == userName)?.Id;
                bool postExists = context.Posts.Any(p => p.Id == postId);

                if (userId == null || !postExists)
                {
                    sb.AppendLine(errorMsg);
                    continue;
                }

                var comment = new Comment()
                {
                    Content = content,
                    UserId = userId.Value,
                    PostId = postId
                };

                comments.Add(comment);
                sb.AppendLine(String.Format(successMsg, $"Comment {content}"));
            }

            context.Comments.AddRange(comments);
            context.SaveChanges();

            string result = sb.ToString().TrimEnd();
            return result;


            //var xDoc = XDocument.Parse(xmlString);
            //var elements = xDoc.Root.Elements();

            //var sb = new StringBuilder();
            //var comments = new List<Comment>();
            //foreach (var e in elements)
            //{
            //    var content = e.Element("content")?.Value;
            //    var user = e.Element("user")?.Value;
            //    var post = e.Element("post")?.Attribute("id")?.Value;

            //    var isValid = !String.IsNullOrWhiteSpace(content) &&
            //                  !String.IsNullOrWhiteSpace(user) &&
            //                  !String.IsNullOrWhiteSpace(post);

            //    if (!isValid)
            //    {
            //        sb.AppendLine(errorMsg);
            //        continue;
            //    }

            //    int? userId = context.Users.FirstOrDefault(u => u.Username == user)?.Id;

            //    var postParsedId = int.Parse(post);
            //    bool postExists = context.Posts.Any(p => p.Id == postParsedId);

            //    if (userId == null || !postExists)
            //    {
            //        sb.AppendLine(errorMsg);
            //        continue;
            //    }

            //    var newComment = new Comment()
            //    {
            //        Content = content,
            //        UserId = userId.Value,
            //        PostId = postParsedId
            //    };

            //    comments.Add(newComment);
            //    sb.AppendLine(String.Format(successMsg, $"Comment {content}."));
            //}

            //context.Comments.AddRange(comments);
            //context.SaveChanges();

            //var result = sb.ToString().TrimEnd();
            //return result;
        }
    }
}
