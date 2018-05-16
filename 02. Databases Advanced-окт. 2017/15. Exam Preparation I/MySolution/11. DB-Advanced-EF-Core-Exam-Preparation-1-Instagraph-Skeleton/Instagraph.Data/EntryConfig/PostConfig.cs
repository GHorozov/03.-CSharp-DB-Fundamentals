namespace Instagraph.Data.EntryConfig
{
    using System;
    using Microsoft.EntityFrameworkCore;
    using Instagraph.Models;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class PostConfig : IEntityTypeConfiguration<Post>
    {
        public void Configure(EntityTypeBuilder<Post> builder)
        {
            builder.HasKey(e => e.Id);

            builder.HasOne(e => e.User)
                .WithMany(p => p.Posts)
                .HasForeignKey(e => e.UserId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}