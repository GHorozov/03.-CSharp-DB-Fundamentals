namespace ProductShop.Data.ConfigurationEntry
{
    using System;
    using ProductShop.Models;
    using Microsoft.EntityFrameworkCore;

    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(e => e.ProductId);

            builder.Property(e => e.Name)
                .IsRequired()
                .IsUnicode(true)
                .HasMaxLength(150);

            builder.Property(e => e.Price)
               .IsRequired();

            builder.HasOne(e => e.Buyer)
                .WithMany(pb => pb.ProductsBought)
                .HasForeignKey(e => e.BuyerId);

            builder.HasOne(e => e.Seller)
                .WithMany(ps => ps.ProductsSold)
                .HasForeignKey(e => e.SellerId);
        }
    }
}
