namespace ProductShop.Data.ConfigurationEntry
{
    using System;
    using ProductShop.Models;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class CategoryProductConfiguration : IEntityTypeConfiguration<CategoryProduct>
    {
        public void Configure(EntityTypeBuilder<CategoryProduct> builder)
        {
            builder.HasKey(e => new { e.ProductId, e.CategoryId });

            builder.HasOne(e => e.Product)
                .WithMany(pc => pc.Categories)
                .HasForeignKey(e => e.ProductId);

            builder.HasOne(e => e.Category)
                .WithMany(pc => pc.Products)
                .HasForeignKey(e => e.CategoryId);
        }
    }
}
