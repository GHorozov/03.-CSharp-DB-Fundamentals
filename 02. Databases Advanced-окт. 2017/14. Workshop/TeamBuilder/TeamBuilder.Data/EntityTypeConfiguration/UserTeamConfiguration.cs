namespace TeamBuilder.Data.EntriesConfiguration
{
    using System;
    using TeamBuilder.Models;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class UserTeamConfiguration : IEntityTypeConfiguration<UserTeam>
    {
        public void Configure(EntityTypeBuilder<UserTeam> builder)
        {
            builder.HasKey(e => new { e.TeamId, e.UserId });

            builder.HasOne(e => e.Team)
                .WithMany(u => u.UserTeams)
                .HasForeignKey(e => e.TeamId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(e => e.User)
               .WithMany(u => u.CreatedUserTeams)
               .HasForeignKey(e => e.UserId)
               .OnDelete(DeleteBehavior.Restrict);
        }
    }
}