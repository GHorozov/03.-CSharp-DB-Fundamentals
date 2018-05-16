namespace P01_StudentSystem.Data.Models.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class CourseConfiguration : IEntityTypeConfiguration<Course>
    {
        public void Configure(EntityTypeBuilder<Course> builder)
        {
            builder.HasKey(e => e.CourseId);

            builder.Property(e => e.Name)
                .IsRequired()
                .IsUnicode(true)
                .HasMaxLength(80);

            builder.Property(e => e.Description)
               .IsRequired(false)
               .IsUnicode(true);

            builder.Property(e => e.StartDate)
               .IsRequired();

            builder.Property(e => e.EndDate)
               .IsRequired();

            builder.Property(e => e.Price)
               .IsRequired();

        }
    }
}
