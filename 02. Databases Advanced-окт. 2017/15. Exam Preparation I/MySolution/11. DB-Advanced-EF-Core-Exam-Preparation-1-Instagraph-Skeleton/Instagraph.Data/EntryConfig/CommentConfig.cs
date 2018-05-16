namespace Instagraph.Data.EntryConfig
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Microsoft.EntityFrameworkCore;
    using Instagraph.Models;

    public class CommentConfig : IEntityTypeConfiguration<Comment>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Comment> builder)
        {
            builder.HasKey(e => e.Id);

            builder.Property(e => e.Content)
                .IsRequired()
                .HasMaxLength(250);

            builder.HasOne(e => e.User)
                .WithMany(c => c.Comments)
                .HasForeignKey(e => e.UserId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}