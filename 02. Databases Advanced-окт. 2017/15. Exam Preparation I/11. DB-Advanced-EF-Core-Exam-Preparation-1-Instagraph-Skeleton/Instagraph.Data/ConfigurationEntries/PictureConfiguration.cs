namespace Instagraph.Data.ConfigurationEntries
{
    using System;
    using Instagraph.Models;
    using Microsoft.EntityFrameworkCore;

    public class PictureConfiguration : IEntityTypeConfiguration<Picture>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Picture> builder)
        {
            builder.HasKey(e => e.Id);

            builder.Property(e => e.Path)
                .IsRequired();

        }
    }
}