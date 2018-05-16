using System;

using Instagraph.Data;
using System.Linq;
using Newtonsoft.Json;
using Microsoft.EntityFrameworkCore;
using System.Xml.Linq;
using System.Collections.Generic;
using Instagraph.DataProcessor.Dto;

namespace Instagraph.DataProcessor
{
    public class Serializer
    {
        public static string ExportUncommentedPosts(InstagraphContext context)
        {
            var uncommentedPosts = context.Posts
                .Where(p => p.Comments.Count == 0)
                .OrderBy(p => p.Id)
                .Select(p => new
                {
                    p.Id,
                    Picture = p.Picture.Path,
                    User = p.User.Username
                })
                .ToArray();


            var json = JsonConvert.SerializeObject(uncommentedPosts, Newtonsoft.Json.Formatting.Indented);

            return json;
        }

        public static string ExportPopularUsers(InstagraphContext context)
        {
            var popularUsers = context.Users
                .Where(u => u.Posts
                    .Any(p => p.Comments
                        .Any(c => u.Followers
                            .Any(f => f.FollowerId == c.UserId))))
                .OrderBy(u => u.Id)
                .Select(u => new
                {
                    Username = u.Username,
                    Followers = u.Followers.Count
                })
                .ToArray();


            var json = JsonConvert.SerializeObject(popularUsers, Newtonsoft.Json.Formatting.Indented);

            return json;
        }

        public static string ExportCommentsOnPosts(InstagraphContext context)
        {
            var users = context.Users
                .Select(u => new
                {
                    Username = u.Username,
                    MostComments = u.Posts.Select(p => p.Comments.Count)
                })
                .ToArray();

            var listUsers = new List<UsersDto>();

            var xDoc = new XDocument();
            xDoc.Add(new XElement("users"));

            foreach (var u in users)
            {
                var mostComments = 0;
                if (u.MostComments.Any())
                {
                    mostComments = u.MostComments.OrderByDescending(c => c).First();
                }

                var newUsersDto = new UsersDto()
                {
                    Username = u.Username,
                    MostComments = mostComments
                };

                listUsers.Add(newUsersDto); 
            }

            listUsers = listUsers.OrderByDescending(u => u.MostComments)
               .ThenBy(u => u.Username).ToList();

            foreach (var u in listUsers)
            {
                xDoc.Root.Add(new XElement("user",
                    new XElement("Username", u.Username),
                    new XElement("MostComments", u.MostComments)));
            }

            var result = xDoc.ToString();
            return result;
        }
    }
}
