namespace PhotoShare.Client.Core.Commands
{
    using System;
    using PhotoShare.Data;
    using Microsoft.EntityFrameworkCore;
    using System.Linq;
    using PhotoShare.Models;

    public class AcceptFriendCommand
    {
        // AcceptFriend <username1> <username2>
        public static string Execute(string[] data)
        {
            var user1Username = data[0];
            var user2Username = data[1];

            using (var context = new PhotoShareContext())
            {
                var user1 = context.Users
                        .Include(u => u.FriendsAdded)
                        .ThenInclude(fa => fa.Friend)
                        .FirstOrDefault(u => u.Username == user1Username);

                var user2 = context.Users
                    .Include(u => u.FriendsAdded)
                    .ThenInclude(fa => fa.Friend)
                    .FirstOrDefault(u => u.Username == user2Username);

                if (user1 == null)
                {
                    throw new ArgumentException($"{user1Username} not found!");
                }

                if (user2 == null)
                {
                    throw new ArgumentException($"{user2Username} not found!");
                }

                var iSUser1HasFriendUser2 = user1.FriendsAdded.Any(u => u.Friend == user2);
                var IsUser2HasFriendUser1 = user2.FriendsAdded.Any(u => u.Friend == user1);

                if(!iSUser1HasFriendUser2 && IsUser2HasFriendUser1)
                {
                    throw new InvalidOperationException($"{user2Username} is already a friend to {user1Username}");
                }

                if(iSUser1HasFriendUser2 && !IsUser2HasFriendUser1)
                {
                    throw new InvalidOperationException($"{user2Username} has not added {user1Username} as a friend");
                }

                //if (!iSUser1HasFriendUser2 && IsUser2HasFriendUser1)
                //{
                //    throw new InvalidOperationException($"{user2Username} has not added {user1Username} as a friend");
                //}

                user2.FriendsAdded.Add(new Friendship()
                {
                    User = user2,
                    Friend = user1
                });

                context.SaveChanges();
                return $"{user1Username} accepted {user2Username} as a friend";
            }
        }
    }
}
