namespace P03_FootballBetting.Data.ModelBuilderConfiguration
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using P03_FootballBetting.Data.Models;

    public class PlayerStatisticConfiguration : IEntityTypeConfiguration<PlayerStatistic>
    {
        public void Configure(EntityTypeBuilder<PlayerStatistic> builder)
        {
            builder.HasKey(e => new { e.GameId, e.PlayerId });

            builder.HasOne(e => e.Game)
                .WithMany(p => p.PlayerStatistics)
                .HasForeignKey(e => e.GameId);

            builder.HasOne(e => e.Player)
                .WithMany(p => p.PlayerStatistics)
                .HasForeignKey(e => e.PlayerId);
        }
    }
}
