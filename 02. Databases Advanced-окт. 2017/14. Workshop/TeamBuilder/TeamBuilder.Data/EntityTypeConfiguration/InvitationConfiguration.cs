namespace TeamBuilder.Data.EntriesConfiguration
{
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Collections.Generic;
    using System.Text;
    using TeamBuilder.Models;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class InvitationConfiguration : IEntityTypeConfiguration<Invitation>
    {
        public void Configure(EntityTypeBuilder<Invitation> builder)
        {
            builder.HasKey(e => e.InvitationId);

            builder.HasOne(e => e.Team)
                .WithMany(i => i.Invitations)
                .HasForeignKey(e => e.TeamId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(e => e.InviteUser)
                .WithMany(iu => iu.ReceivedInvitations)
                .HasForeignKey(r => r.InvitedUserId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}