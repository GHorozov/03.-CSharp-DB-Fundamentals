namespace TeamBuilder.Data.EntriesConfiguration
{
    using Microsoft.EntityFrameworkCore;
    using TeamBuilder.Models;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(e => e.UserId);

            builder.HasAlternateKey(e => e.Username);

            builder.Property(e => e.Username)
                .IsRequired()
                .IsUnicode(true)
                .HasMaxLength(25);

            builder.HasIndex(e => e.Username)
                .IsUnique();

            builder.Property(e => e.FirstName)
                .HasMaxLength(25);

            builder.Property(e => e.LastName)
                .HasMaxLength(25);

            builder.Property(e => e.Password)
                .IsRequired()
                .HasMaxLength(30);

            builder.Property(e => e.Age)
                .HasDefaultValue(null);
        }
    }
}