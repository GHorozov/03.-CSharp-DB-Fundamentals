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
            string tag = data[0].ValidateOrTransform();

            using (PhotoShareContext context = new PhotoShareContext())
            {
                var checkTag = context.Tags.Where(t => t.Name == tag).FirstOrDefault();

                if (checkTag != null)
                {
                    throw new ArgumentException($"Tag {tag} exists!");
                }

                context.Tags.Add(new Tag
                {
                    Name = tag
                });

                context.SaveChanges();
            }

            return $"Tag {tag} was added successfully to database!";
        }
    }
}
