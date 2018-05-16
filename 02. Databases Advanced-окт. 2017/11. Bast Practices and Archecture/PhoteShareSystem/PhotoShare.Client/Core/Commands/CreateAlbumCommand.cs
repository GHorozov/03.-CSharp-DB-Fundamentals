namespace PhotoShare.Client.Core.Commands
{
    using System;
    using PhotoShare.Data;
    using System.Linq;
    using PhotoShare.Models;
    using PhotoShare.Client.Utilities;

    public class CreateAlbumCommand
    {
        // CreateAlbum <username> <albumTitle> <BgColor> <tag1> <tag2>...<tagN>
        public static string Execute(params string[] data)
        {
            var username = data[0];
            var albumTitle = data[1];
            var bgColor = data[2];
            string[] inputTags = data.Skip(3).Select(t => t.ValidateOrTransform()).ToArray();

            using (var context = new PhotoShareContext())
            {
                var user = context.Users.Where(u => u.Username == username).FirstOrDefault();

                if (user == null)
                {
                    throw new ArgumentException($"User {username} not found!");
                }

                var albumCheck = context.Albums.Where(a => a.Name == albumTitle).FirstOrDefault();

                if (albumCheck != null)
                {
                    throw new ArgumentException($"Album {albumTitle} exists!");
                }

                Color color;
                if (!Enum.TryParse(bgColor, out color))
                {
                    throw new ArgumentException($"Color {bgColor} not found!");
                }

                if (!inputTags.All(t => context.Tags.Any(ct => ct.Name == t)))
                {
                    throw new ArgumentException($"Invalid tags!");
                }

                context.Albums.Add(new Album()
                {
                    Name = albumTitle,
                    BackgroundColor = color,
                    AlbumTags = inputTags.Select(t => new AlbumTag()
                    {
                        Tag = context.Tags.FirstOrDefault(ct => ct.Name == t)
                    })
                    .ToArray(),
                });

                context.SaveChanges();
                return $"Album {albumTitle} successfully created!";
            }

        }
    }
}
