namespace TeamBuilder.Data.EntriesConfiguration
{
    using System;
    using Microsoft.EntityFrameworkCore;
    using TeamBuilder.Models;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class EventTeamConfiguration : IEntityTypeConfiguration<EventTeam>
    {
        public void Configure(EntityTypeBuilder<EventTeam> builder)
        {
            builder.HasKey(e => new { e.EventId, e.TeamId });

            builder.HasOne(e => e.Event)
                .WithMany(et => et.EventTeams)
                .HasForeignKey(e => e.EventId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(e => e.Team)
                .WithMany(et => et.EventTeams)
                .HasForeignKey(e => e.TeamId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}