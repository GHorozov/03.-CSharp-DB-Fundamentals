namespace BookShop.Data.EntriesConfiguration
{
    using System;
    using BookShop.Models;
    using Microsoft.EntityFrameworkCore;  
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class ConfigurationBookCategory : IEntityTypeConfiguration<BookCategory>
    {
        public void Configure(EntityTypeBuilder<BookCategory> builder)
        {
            builder.HasKey(e => new { e.BookId, e.CategoryId });

            builder.HasOne(e => e.Book)
                .WithMany(bc => bc.BookCategories)
                .HasForeignKey(e => e.BookId);

            builder.HasOne(e => e.Category)
                .WithMany(cb => cb.CategoryBooks)
                .HasForeignKey(e => e.CategoryId);
        }
    }
}
