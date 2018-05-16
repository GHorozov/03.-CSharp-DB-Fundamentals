namespace TeamBuilder.Data.EntriesConfiguration
{
    using System;
    using Microsoft.EntityFrameworkCore;
    using TeamBuilder.Models;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class TeamConfiguration : IEntityTypeConfiguration<Team>
    {
        public void Configure(EntityTypeBuilder<Team> builder)
        {
            builder.HasKey(e => e.TeamId);

            builder.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(25);

            builder.HasIndex(e => e.Name)
                .IsUnique();

            builder.Property(e => e.Description)
                .HasMaxLength(32);

            builder.Property(e => e.Acronym)
                .IsRequired()
                .HasMaxLength(3);

            builder.HasOne(e => e.Creator)
                .WithMany(t => t.Teams)
                .HasForeignKey(e => e.CreatorId);
        }
    }
}