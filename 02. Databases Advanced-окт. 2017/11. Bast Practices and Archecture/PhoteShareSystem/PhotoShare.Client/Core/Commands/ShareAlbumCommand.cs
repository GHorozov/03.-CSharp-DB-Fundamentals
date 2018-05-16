namespace PhotoShare.Client.Core.Commands
{
    using PhotoShare.Data;
    using PhotoShare.Models;
    using System;
    using System.Linq;
    using Microsoft.EntityFrameworkCore;

    public class ShareAlbumCommand
    {
        // ShareAlbum <albumId> <username> <permission>
        // For example:
        // ShareAlbum 4 dragon321 Owner
        // ShareAlbum 4 dragon11 Viewer
        public static string Execute(params string[] data)
        {
            var albumId = int.Parse(data[0]);
            var username = data[1];
            var permission = data[2];

            using(var context = new PhotoShareContext())
            {
                var albumIdParam = context.Albums
                    .Include(u => u.AlbumRoles)
                    .FirstOrDefault(a => a.Id == albumId);

                if(albumIdParam == null)
                {
                    throw new ArgumentException($"Album {albumId} not found!");
                }

                var user = context.Users.FirstOrDefault(u => u.Username == username);

                if(user == null)
                {
                    throw new ArgumentException($"User {username} not found!");
                }

                if(permission != "Owner" && permission != "Viewer")
                {
                    throw new ArgumentException("Permission must be either “Owner” or “Viewer”!");
                }

                var album = context.Albums.FirstOrDefault(a => a.Id == albumId);

                Role role = new Role();

                if (permission == "Owner")
                {
                    role = Role.Owner;
                }
                else
                {
                    role = Role.Viewer;
                }

                AlbumRole albRole = new AlbumRole();

                albRole.Album = album;
                albRole.User = user;
                albRole.Role = role;

                context.AlbumRoles.Add(albRole);

                context.SaveChanges();
                return $"Username {user.Username} added to album {albumIdParam.Name} ({permission})";
            }
        }
    }
}
