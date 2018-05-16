namespace P03_FootballBetting.Data.ModelBuilderConfiguration
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using P03_FootballBetting.Data.Models;

    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(e => e.UserId);

            builder.Property(e => e.Name)
                .HasMaxLength(100);

            builder.Property(e => e.Username)
                .IsRequired()
                .HasMaxLength(3);

            builder.Property(e => e.Password)
                .IsRequired()
                .HasMaxLength(20);

            builder.Property(e => e.Email)
                .IsRequired()
                .HasMaxLength(60);
        }
    }
}
