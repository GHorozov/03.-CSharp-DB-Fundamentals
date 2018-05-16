namespace Instagraph.Data.ConfigurationEntries
{
    using System;
    using Instagraph.Models;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class CommentConfiguration : IEntityTypeConfiguration<Comment>
    {
        public void Configure(EntityTypeBuilder<Comment> builder)
        {
            builder.HasKey(e => e.Id);

            builder.Property(e => e.Content)
                .IsRequired()
                .HasMaxLength(250);

            builder.HasOne(e => e.User)
                .WithMany(c => c.Comments)
                .HasForeignKey(e => e.UserId);

            builder.HasOne(e => e.Post)
                .WithMany(c => c.Comments)
                .HasForeignKey(e => e.PostId);
        }
    }
}