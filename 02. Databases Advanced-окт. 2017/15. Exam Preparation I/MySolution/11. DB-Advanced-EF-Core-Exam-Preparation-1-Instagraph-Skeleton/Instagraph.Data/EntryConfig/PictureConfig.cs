namespace Instagraph.Data.EntryConfig
{
    using System;
    using Microsoft.EntityFrameworkCore;
    using Instagraph.Models;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class PictureConfig : IEntityTypeConfiguration<Picture>
    {
        public void Configure(EntityTypeBuilder<Picture> builder)
        {
            builder.HasKey(e => e.Id);
        }
    }
}