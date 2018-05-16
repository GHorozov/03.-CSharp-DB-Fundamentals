namespace FastFood.Data.EntryConfigurations
{
    using FastFood.Models;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class ItemConfig : IEntityTypeConfiguration<Item>
    {
        public void Configure(EntityTypeBuilder<Item> builder)
        {
            builder.HasKey(e => e.Id);

            builder.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(30);

            builder.HasAlternateKey(e => e.Name);

            builder.Property(e => e.Price)
                .IsRequired();

            builder.HasOne(e => e.Category)
                .WithMany(i => i.Items)
                .HasForeignKey(e => e.CategoryId);
        }
    }
}