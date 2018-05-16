namespace P03_FootballBetting.Data.ModelBuilderConfiguration
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using P03_FootballBetting.Data.Models;

    public class PlayerConfiguration : IEntityTypeConfiguration<Player>
    {
        public void Configure(EntityTypeBuilder<Player> builder)
        {
            builder.HasKey(e => e.PlayerId);

            builder.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(e => e.IsInjured)
                .HasDefaultValue(false);

            builder.HasOne(e => e.Team)
                .WithMany(p => p.Players)
                .HasForeignKey(e => e.TeamId);

            builder.HasOne(e => e.Position)
                .WithMany(p => p.Players)
                .HasForeignKey(e => e.PositionId);
        }
    }
}
