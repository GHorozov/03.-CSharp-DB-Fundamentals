namespace ProductShop.Data.ConfigurationEntry
{
    using System;
    using ProductShop.Models;
    using Microsoft.EntityFrameworkCore;

    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Category> builder)
        {
            builder.HasKey(e => e.CategoryId);

            builder.Property(e => e.Name)
                .IsRequired()
                .IsUnicode(true)
                .HasMaxLength(15);     
        }
    }
}
