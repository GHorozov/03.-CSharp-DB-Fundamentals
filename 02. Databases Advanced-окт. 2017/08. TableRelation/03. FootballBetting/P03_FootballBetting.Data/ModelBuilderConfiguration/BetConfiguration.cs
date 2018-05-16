namespace P03_FootballBetting.Data.ModelBuilderConfiguration
{
    using Microsoft.EntityFrameworkCore;
    using P03_FootballBetting.Data.Models;

    public class BetConfiguration : IEntityTypeConfiguration<Bet>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Bet> builder)
        {
            builder.HasKey(e => e.BetId);

            builder.HasOne(e => e.Game)
                .WithMany(b => b.Bets)
                .HasForeignKey(e => e.GameId);

            builder.HasOne(e => e.User)
                .WithMany(b => b.Bets)
                .HasForeignKey(e => e.UserId);
        }
    }
}
