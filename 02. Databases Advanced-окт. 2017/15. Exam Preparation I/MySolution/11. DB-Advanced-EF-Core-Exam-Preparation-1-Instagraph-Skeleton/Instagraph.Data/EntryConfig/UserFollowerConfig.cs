namespace Instagraph.Data.EntryConfig
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Microsoft.EntityFrameworkCore;
    using Instagraph.Models;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class UserFollowerConfig : IEntityTypeConfiguration<UserFollower>
    {
        public void Configure(EntityTypeBuilder<UserFollower> builder)
        {
            builder.HasKey(e => new { e.UserId, e.FollowerId });

            builder.HasOne(e => e.User)
                .WithMany(f => f.Followers)
                .HasForeignKey(e => e.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(e => e.Follower)
              .WithMany(f => f.UsersFollowing)
              .HasForeignKey(e => e.FollowerId)
              .OnDelete(DeleteBehavior.Restrict);
        }
    }
}