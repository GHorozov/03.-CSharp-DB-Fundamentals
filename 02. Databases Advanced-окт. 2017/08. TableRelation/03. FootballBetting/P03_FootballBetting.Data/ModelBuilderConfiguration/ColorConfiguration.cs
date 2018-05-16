namespace P03_FootballBetting.Data.ModelBuilderConfiguration
{
    using Microsoft.EntityFrameworkCore;
    using P03_FootballBetting.Data.Models;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class ColorConfiguration : IEntityTypeConfiguration<Color>
    {
        public void Configure(EntityTypeBuilder<Color> builder)
        {
            builder.HasKey(e => e.ColorId);

            builder.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(40);
        }
    }
}
