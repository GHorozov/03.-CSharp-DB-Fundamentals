namespace Instagraph.Data.EntryConfig
{
    using System;
    using Instagraph.Models;
    using Microsoft.EntityFrameworkCore;
   
    public class UserConfig : IEntityTypeConfiguration<User>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<User> builder)
        {
            builder.HasKey(e => e.Id);

            builder.HasAlternateKey(e => e.Username);

            builder.Property(e => e.Username)
                .IsRequired()
                .HasMaxLength(30);

            builder.Property(e => e.Password)
                .IsRequired()
                .HasMaxLength(20);


        }
    }
}