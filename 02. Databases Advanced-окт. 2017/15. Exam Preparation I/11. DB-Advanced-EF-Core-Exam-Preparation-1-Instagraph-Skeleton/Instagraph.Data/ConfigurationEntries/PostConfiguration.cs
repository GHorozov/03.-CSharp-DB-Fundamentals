namespace Instagraph.Data.ConfigurationEntries
{
    using System;
    using Instagraph.Models;
    using Microsoft.EntityFrameworkCore;

    public class PostConfiguration : IEntityTypeConfiguration<Post>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Post> builder)
        {
            builder.HasKey(e => e.Id);

            builder.Property(e => e.Caption)
                .IsRequired();

            builder.HasOne(e => e.User)
                .WithMany(p => p.Posts)
                .HasForeignKey(e => e.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(e => e.Picture)
                .WithMany(p => p.Posts)
                .HasForeignKey(e => e.PictureId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}