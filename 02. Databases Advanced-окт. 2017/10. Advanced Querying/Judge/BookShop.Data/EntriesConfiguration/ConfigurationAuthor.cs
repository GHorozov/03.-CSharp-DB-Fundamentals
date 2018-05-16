namespace BookShop.Data.EntriesConfiguration
{
    using System;
    using BookShop.Models;
    using Microsoft.EntityFrameworkCore;
    
    public class ConfigurationAuthor : IEntityTypeConfiguration<Author>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Author> builder)
        {
            builder.HasKey(e => e.AuthorId);

            builder.Property(e => e.FirstName)
                .IsRequired(false)
                .IsUnicode(true)
                .HasMaxLength(50);

            builder.Property(e => e.LastName)
               .IsRequired()
               .IsUnicode(true)
               .HasMaxLength(50);
        }
    }
}
