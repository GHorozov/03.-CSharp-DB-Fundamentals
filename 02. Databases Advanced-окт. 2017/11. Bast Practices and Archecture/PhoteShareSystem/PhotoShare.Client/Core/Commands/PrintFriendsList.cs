namespace PhotoShare.Client.Core.Commands
{
    using System;
    using System.Linq;
    using PhotoShare.Data;
    using Microsoft.EntityFrameworkCore;

    public class PrintFriendsListCommand 
    {
        // PrintFriendsList <username>
        public static string Execute(string[] data)
        {
            var username = data[0];

            using(var context = new PhotoShareContext())
            {
                var user = context.Users
                    .Include(u => u.FriendsAdded)
                    .ThenInclude(fa => fa.Friend)
                    .FirstOrDefault(u => u.Username == username);

                if(user == null)
                {
                    throw new ArgumentException($"User {username} is not found!");
                }

                var userfriends = user.FriendsAdded.Select(f => f.Friend.Username).OrderBy(f => f).ToArray();

                foreach (var friend in userfriends)
                {
                    return $"-{friend}";
                }

                return $"No friends for this user. :(";
            }
        }
    }
}
