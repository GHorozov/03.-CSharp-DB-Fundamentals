namespace Instagraph.DataProcessor
{
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

    public class Deserializer
    {
        private static string errorMesg = "Error: Invalid data.";
        private static string sucessMesg = "Successfully imported {0}";

        public static string ImportPictures(InstagraphContext context, string jsonString)
        {
            var pictures = JsonConvert.DeserializeObject<Picture[]>(jsonString);

            var validetedPictures = new List<Picture>();
            var sb = new StringBuilder();
            foreach (var pic in pictures)
            {
                var isvalid = !String.IsNullOrWhiteSpace(pic.Path)  && pic.Size > 0;

                var pictureExist = context.Pictures.Any(p => p.Path == pic.Path) ||
                    validetedPictures.Any(p => p.Path == pic.Path); // is exist in database or in validatedPictures

                if (!isvalid || pictureExist)
                {
                    sb.AppendLine(errorMesg); continue;
                }

                validetedPictures.Add(pic);
                sb.AppendLine(string.Format(sucessMesg, $"Picture {pic.Path}"));
            }

            context.Pictures.AddRange(validetedPictures);
            context.SaveChanges();

            return sb.ToString().Trim();
        }

        public static string ImportUsers(InstagraphContext context, string jsonString)
        {
            var users = JsonConvert.DeserializeObject<UserDto[]>(jsonString);

            var validatedUsers = new List<User>();
            var sb = new StringBuilder();
            foreach (var userDto in users)
            {
                var isValid = !String.IsNullOrWhiteSpace(userDto.Username) &&
                    userDto.Username.Length <= 30 &&
                    !String.IsNullOrWhiteSpace(userDto.Password) &&
                    userDto.Password.Length <= 20 &&
                    !String.IsNullOrWhiteSpace(userDto.ProfilePicture);

                var picture = context.Pictures.FirstOrDefault(p => p.Path == userDto.ProfilePicture);
                var userExist = validatedUsers.Any(u => u.Username == userDto.Username);


                if (!isValid || picture == null || userExist)
                {
                    sb.AppendLine(errorMesg); continue;
                }

                var userMap = Mapper.Map<User>(userDto);
                userMap.ProfilePicture = picture;
                validatedUsers.Add(userMap);
                sb.AppendLine(String.Format(sucessMesg, $"User {userDto.Username}"));
            }

            context.Users.AddRange(validatedUsers);
            context.SaveChanges();

            return sb.ToString().Trim();
        }

        public static string ImportFollowers(InstagraphContext context, string jsonString)
        {
            throw new NotImplementedException();
        }

        public static string ImportPosts(InstagraphContext context, string xmlString)
        {
            throw new NotImplementedException();
        }

        public static string ImportComments(InstagraphContext context, string xmlString)
        {
            throw new NotImplementedException();
        }
    }
}
