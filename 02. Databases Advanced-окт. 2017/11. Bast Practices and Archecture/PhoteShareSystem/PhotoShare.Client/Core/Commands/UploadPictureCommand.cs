namespace PhotoShare.Client.Core.Commands
{
    using System;
    using System.Linq;
    using PhotoShare.Data;
    using PhotoShare.Models;

    public class UploadPictureCommand
    {
        // UploadPicture <albumName> <pictureTitle> <pictureFilePath>
        public static string Execute(string[] data)
        {
            var albumName = data[0];
            var pictureTitle = data[1];
            var pictureFilePath = data[2];

            using (var context = new PhotoShareContext())
            {
                var album = context.Albums.FirstOrDefault(a => a.Name == albumName);

                if(album == null)
                {
                    throw new ArgumentException($"Album {albumName} not found!");
                }

                album.Pictures.Add(new Picture()
                {
                    Title = pictureTitle,
                    Path = pictureFilePath
                });

                context.SaveChanges();
                return $"Picture {pictureTitle} added to {albumName}!";
            }
        }
    }
}
