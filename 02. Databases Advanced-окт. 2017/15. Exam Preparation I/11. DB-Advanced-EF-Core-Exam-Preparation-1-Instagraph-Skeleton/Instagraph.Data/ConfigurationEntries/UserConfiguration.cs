namespace Instagraph.Data.ConfigurationEntries
{
    using System;
    using Instagraph.Models;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(e => e.Id);

            builder.HasAlternateKey(e => e.Username);

            builder.Property(e => e.Username)
                .IsRequired()
                .HasMaxLength(30);

            builder.Property(e => e.Password)
                .IsRequired()
                .HasMaxLength(20);

            builder.HasOne(e => e.ProfilePicture)
                .WithMany(u => u.Users)
                .HasForeignKey(e => e.ProfilePictureId);
        }
    }
}