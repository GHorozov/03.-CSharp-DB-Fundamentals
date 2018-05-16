namespace FastFood.Data.EntryConfigurations
{
    using FastFood.Models;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class OrderItemConfig : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            builder.HasKey(e => new { e.OrderId, e.ItemId });

            builder.HasOne(e => e.Order)
                .WithMany(oi => oi.OrderItems)
                .HasForeignKey(e => e.OrderId);

            builder.HasOne(e => e.Item)
                .WithMany(oi => oi.OrderItems)
                .HasForeignKey(e => e.ItemId);
        }
    }
}