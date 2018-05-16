namespace P01_StudentSystem.Data.Models.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using System;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class StudentConfiguration : IEntityTypeConfiguration<Student>
    {
        public void Configure(EntityTypeBuilder<Student> builder)
        {

            builder.HasKey(e => e.StudentId);

            builder.Property(e => e.Name)
                    .IsRequired()
                    .IsUnicode(true)
                    .HasMaxLength(100);

            builder.Property(e => e.PhoneNumber)
                    .IsRequired(false)
                    .IsUnicode(false)
                    .HasMaxLength(10);

            builder.Property(e => e.RegisteredOn)
                    .IsRequired()
                    .HasDefaultValueSql("GETDATE()");

            builder.Property(e => e.Birthday)
                    .IsRequired(false);

        }
    }
}
