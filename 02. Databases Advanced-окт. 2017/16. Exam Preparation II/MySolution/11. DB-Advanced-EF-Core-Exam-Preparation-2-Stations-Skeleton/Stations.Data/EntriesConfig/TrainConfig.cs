namespace Stations.Data.EntriesConfig
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Stations.Models;

    public class TrainConfig : IEntityTypeConfiguration<Train>
    {
        public void Configure(EntityTypeBuilder<Train> builder)
        {
            builder.HasKey(e => e.Id);

            builder.HasAlternateKey(e => e.TrainNumber);

            builder.Property(e => e.TrainNumber)
                .IsRequired()
                .HasMaxLength(10);
        }
    }
}