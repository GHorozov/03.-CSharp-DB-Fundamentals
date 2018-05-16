namespace BookShop.Data.EntriesConfiguration
{
    using System;
    using BookShop.Models;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class ConfigurationBook : IEntityTypeConfiguration<Book>
    {
        public void Configure(EntityTypeBuilder<Book> builder)
        {
            builder.HasKey(e => e.BookId);

            builder.Property(e => e.Title)
                .IsRequired()
                .IsUnicode(true)
                .HasMaxLength(50);

            builder.Property(e => e.Description)
                .IsRequired()
                .IsUnicode(true)
                .HasMaxLength(1000);

            builder.Property(e => e.ReleaseDate)
                .IsRequired(false);

            builder.Property(e => e.Copies)
                .IsRequired();

            builder.Property(e => e.Price)
                .IsRequired();

            builder.HasOne(e => e.Author)
                .WithMany(b => b.Books)
                .HasForeignKey(e => e.AuthorId);
        }
    }
}
