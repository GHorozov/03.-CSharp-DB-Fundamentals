namespace PhotoShare.Client.Core.Commands
{
    using System;
    using System.Linq;
    using PhotoShare.Data;
    using PhotoShare.Models;
    using Microsoft.EntityFrameworkCore;

    public class AddFriendCommand
    {
        // AddFriend <username1> <username2>
        public static string Execute(string[] data)
        {
            var user = data[0];
            var friend = data[1];

            using (var context = new PhotoShareContext())
            {
                var user1 = context.Users
                    .Include(u => u.FriendsAdded)
                    .ThenInclude(fa => fa.Friend)
                    .FirstOrDefault(u => u.Username == user);

                var user2 = context.Users
                    .Include(u => u.FriendsAdded)
                    .ThenInclude(fa => fa.Friend)
                    .FirstOrDefault(u => u.Username == friend);

                if (user1 == null)
                {
                    throw new ArgumentException($"{user} not found!");
                }

                if (user2 == null)
                {
                    throw new ArgumentException($"{friend} not found!");
                }

                var addedUser = user1.FriendsAdded.Any(u => u.Friend == user2);
                var acceptedFriend = user2.FriendsAdded.Any(u => u.Friend == user1);
               
                //first is added second and second is friend with first
                if (addedUser && acceptedFriend)
                {
                    throw new InvalidOperationException($"{friend} is already a friend to {user}");
                }
               
                //first is not accepted friendship with second
                if (addedUser && !acceptedFriend)
                {
                    throw new InvalidOperationException("Friend request is already send!");
                }

                //first has request for friendship from second
                if (!addedUser && acceptedFriend)
                {
                    throw new InvalidOperationException($"{user} already receive friend request from {friend}");
                }

                user1.FriendsAdded.Add(new Friendship()
                {
                    User = user1,
                    Friend = user2
                });

                context.SaveChanges();
                return $"Friend {friend} added to {user}";
            }
        }
    }
}
