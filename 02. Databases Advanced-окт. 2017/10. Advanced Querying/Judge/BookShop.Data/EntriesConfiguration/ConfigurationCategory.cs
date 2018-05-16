namespace BookShop.Data.EntriesConfiguration
{
    using System;
    using BookShop.Models;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class ConfigurationCategory : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasKey(e => e.CategoryId);

            builder.Property(e => e.Name)
                .IsRequired()
                .IsUnicode(true)
                .HasMaxLength(50);
        }
    }
}
