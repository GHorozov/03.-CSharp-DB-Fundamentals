namespace PhotoShare.Client.Core.Commands
{
    using System;
    using PhotoShare.Data;
    using System.Linq;
    using PhotoShare.Models;

    public class AddTagToCommand 
    {
        // AddTagTo <albumName> <tag>
        public static string Execute(params string[] data)
        {
            var albumName = data[0];
            var tag = data[1];

            using(var context = new PhotoShareContext())
            {
                if(!context.Albums.Any(a => a.Name == albumName))
                {
                    throw new ArgumentException("Either tag or album do not exist!");
                }

                if(!context.Tags.Any(t => t.Name == "#"+tag))
                {
                    throw new ArgumentException("Either tag or album do not exist!");
                }

                var album = context.Albums.SingleOrDefault(a => a.Name == albumName);

                //context.Tags.Add(new Tag()
                //{
                //    Name = tag
                //});

                var findTag = context.Tags.SingleOrDefault(t => t.Name == "#" + tag);

                context.AlbumTags.Add(new AlbumTag()
                {
                    Album = album,
                    Tag = findTag
                });

                context.SaveChanges();

                return $"Tag {tag} added to {albumName}!";
            }
        }
    }
}
