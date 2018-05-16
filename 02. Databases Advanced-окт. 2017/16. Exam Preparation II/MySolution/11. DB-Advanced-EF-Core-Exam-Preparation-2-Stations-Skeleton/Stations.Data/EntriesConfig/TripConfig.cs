namespace Stations.Data.EntriesConfig
{
    using System;
    using Microsoft.EntityFrameworkCore;
    using Stations.Models;

    public class TripConfig : IEntityTypeConfiguration<Trip>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Trip> builder)
        {
            builder.HasKey(e => e.Id);
        }
    }
}