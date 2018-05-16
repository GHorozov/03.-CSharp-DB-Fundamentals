namespace TeamBuilder.Data.EntriesConfiguration
{
    using System;
    using Microsoft.EntityFrameworkCore;
    using TeamBuilder.Models;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using System.ComponentModel.DataAnnotations.Schema;

    public class EventConfiguration : IEntityTypeConfiguration<Event>
    {
        public void Configure(EntityTypeBuilder<Event> builder)
        {
            builder.HasKey(e => e.EventId);

            builder.Property(e => e.Name)
                .IsRequired()
                .IsUnicode(true)
                .HasMaxLength(25);

            builder.Property(e => e.Description)
                .HasMaxLength(250);

            builder.HasOne(e => e.Creator)
                .WithMany(e => e.CreatedEvents)
                .HasForeignKey(e => e.CreatorId);
        }
    }
}