using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
namespace P01_StudentSystem.Data.Models.Configurations
{
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class HomeworkConfiguration : IEntityTypeConfiguration<Homework>
    {
        public void Configure(EntityTypeBuilder<Homework> builder)
        {
            builder.HasKey(e => e.HomeworkId);

            builder.Property(e => e.Content)
                .IsRequired()
                .IsUnicode(false);

            builder.Property(e => e.SubmissionTime)
                .IsRequired();

            builder.HasKey(e => new { e.StudentId, e.CourseId });

            builder.HasOne(sc => sc.Student)
                .WithMany(s => s.HomeworkSubmissions)
                .HasForeignKey(sc => sc.StudentId);

            builder.HasOne(sc => sc.Course)
                .WithMany(s => s.HomeworkSubmissions)
                .HasForeignKey(sc => sc.CourseId);
        }
    }
}
