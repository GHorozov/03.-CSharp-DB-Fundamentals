namespace Instagraph.Data.ConfigurationEntries
{
    using System;
    using Instagraph.Models;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class UserFollowerConfiguration : IEntityTypeConfiguration<UserFollower>
    {
        public void Configure(EntityTypeBuilder<UserFollower> builder)
        {
            builder.HasKey(e => new { e.UserId, e.FollowerId });

            builder.HasOne(e => e.User)
                .WithMany(uc => uc.Followers)
                .HasForeignKey(e => e.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(e => e.Follower)
                .WithMany(u => u.UsersFollowing)
                .HasForeignKey(e => e.FollowerId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}