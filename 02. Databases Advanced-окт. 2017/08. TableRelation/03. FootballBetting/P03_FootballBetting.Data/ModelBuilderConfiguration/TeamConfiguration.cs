namespace P03_FootballBetting.Data.ModelBuilderConfiguration
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using P03_FootballBetting.Data.Models;

    public class TeamConfiguration : IEntityTypeConfiguration<Team>
    {
        public void Configure(EntityTypeBuilder<Team> builder)
        {
            builder.HasKey(e => e.TeamId);

            builder.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(80);

            builder.Property(e => e.Initials)
                .IsRequired()
                .HasColumnType("NCHAR(3)");

            builder.Property(e => e.LogoUrl)
                .IsUnicode(false);

            builder.HasOne(e => e.PrimaryKitColor)
                .WithMany(c => c.PrimaryKitTeams)
                .HasForeignKey(e => e.PrimaryKitColorId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(e => e.SecondaryKitColor)
                .WithMany(c => c.SecondaryKitTeams)
                .HasForeignKey(e => e.SecondaryKitColorId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(e => e.Town)
                .WithMany(t => t.Teams)
                .HasForeignKey(e => e.TownId);
        }
    }
}
