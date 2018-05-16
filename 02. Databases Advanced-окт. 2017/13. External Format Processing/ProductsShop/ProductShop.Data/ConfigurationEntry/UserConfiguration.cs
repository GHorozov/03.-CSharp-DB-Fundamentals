namespace ProductShop.Data.ConfigurationEntry
{
    using System;
    using ProductShop.Models;

    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(e => e.UserId);

            builder.Property(e => e.FirstName)
                .IsRequired(false)
                .IsUnicode(true)
                .HasMaxLength(100);

            builder.Property(e => e.LastName)
               .IsRequired()
               .IsUnicode(true)
               .HasMaxLength(100);

            builder.Property(e => e.Age)
               .IsRequired(false);
        }
    }
}
