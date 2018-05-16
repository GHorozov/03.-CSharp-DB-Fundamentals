namespace P03_FootballBetting.Data.ModelBuilderConfiguration
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using P03_FootballBetting.Data.Models;

    public class GameConfiguration : IEntityTypeConfiguration<Game>
    {
        public void Configure(EntityTypeBuilder<Game> builder)
        {
            builder.HasKey(e => e.GameId);

            builder.HasOne(e => e.HomeTeam)
                .WithMany(h => h.HomeGames)
                .HasForeignKey(e => e.HomeTeamId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(e => e.AwayTeam)
              .WithMany(h => h.AwayGames)
              .HasForeignKey(e => e.AwayTeamId)
              .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
