namespace FastFood.Data.EntryConfigurations
{
    using FastFood.Models;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class OrderConfig : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.HasKey(e => e.Id);

            builder.Property(e => e.Customer)
                .IsRequired();

            builder.Property(e => e.DateTime)
                .IsRequired();

            //builder.HasOne(e => e.Employee)
            //    .WithMany(e => e.)
            //    .HasForeignKey(e => e.)
        }
    }
}