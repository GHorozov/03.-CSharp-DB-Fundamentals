namespace Stations.Data.EntriesConfig
{
    using System;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Stations.Models;

    public class SeatingClassConfig : IEntityTypeConfiguration<SeatingClass>
    {
        public void Configure(EntityTypeBuilder<SeatingClass> builder)
        {
            builder.HasKey(e => e.Id);

            builder.HasAlternateKey(e => e.Name);

            builder.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(30);

            builder.HasAlternateKey(e => e.Abbreviation);

            builder.Property(e => e.Abbreviation)
                .IsRequired()
                .HasMaxLength(2);
        }
    }
}