namespace Stations.Data.EntriesConfig
{
    using System;
    using Microsoft.EntityFrameworkCore;
    using Stations.Models;

    public class StationConfig : IEntityTypeConfiguration<Station>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Station> builder)
        {
            builder.HasKey(e => e.Id);

            builder.HasAlternateKey(e => e.Name);

            builder.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(e => e.Town)
               .IsRequired()
               .HasMaxLength(50);

            builder.HasMany(e => e.TripsTo)
               .WithOne(s => s.DestinationStation)
               .HasForeignKey(s => s.DestinationStationId)
               .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(e => e.TripsFrom)
              .WithOne(s => s.OriginStation)
              .HasForeignKey(s => s.OriginStationId)
              .OnDelete(DeleteBehavior.Restrict);
        }
    }
}