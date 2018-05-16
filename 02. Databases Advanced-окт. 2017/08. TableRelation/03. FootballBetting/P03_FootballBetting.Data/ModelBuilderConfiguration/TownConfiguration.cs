namespace P03_FootballBetting.Data.ModelBuilderConfiguration
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using P03_FootballBetting.Data.Models;

    public class TownConfiguration : IEntityTypeConfiguration<Town>
    {
        public void Configure(EntityTypeBuilder<Town> builder)
        {
            builder.HasKey(e => e.TownId);

            builder.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(80);

            builder.HasOne(e => e.Country)
                .WithMany(t => t.Towns)
                .HasForeignKey(e => e.CountryId);
        }
    }
}
