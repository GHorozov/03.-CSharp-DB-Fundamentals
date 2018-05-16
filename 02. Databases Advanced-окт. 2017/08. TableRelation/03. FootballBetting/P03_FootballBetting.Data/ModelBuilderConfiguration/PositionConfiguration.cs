namespace P03_FootballBetting.Data.ModelBuilderConfiguration
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using P03_FootballBetting.Data.Models;

    public class PositionConfiguration : IEntityTypeConfiguration<Position>
    {
        public void Configure(EntityTypeBuilder<Position> builder)
        {
            builder.HasKey(e => e.PositionId);

            builder.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(30);
        }
    }
}
