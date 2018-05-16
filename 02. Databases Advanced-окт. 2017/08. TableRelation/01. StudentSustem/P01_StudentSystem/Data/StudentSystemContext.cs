namespace P01_StudentSystem.Data 
{
    using Microsoft.EntityFrameworkCore;
    using P01_StudentSystem.Data.Models;
    using P01_StudentSystem.Data.Models.Configurations;

    public class StudentSystemContext : DbContext
    {
        public StudentSystemContext()
        {
        }

        public StudentSystemContext(DbContextOptions options)
            : base(options)
        {
        }

        public DbSet<Student> Students { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<StudentCourse> StudentCourses { get; set; }
        public DbSet<Homework> HomeworkSubmissions { get; set; }
        public DbSet<Resource> Resources { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(DBContextConfiguration.ConfigurationString);
            }
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            //Student
            builder.ApplyConfiguration(new StudentConfiguration());

            //Corse
            builder.ApplyConfiguration(new CourseConfiguration());

            //StudentCourse
            builder.ApplyConfiguration(new StudentCourseConfiguration());

            //HomeworkSubmission
            builder.ApplyConfiguration(new HomeworkConfiguration());

            //Resource
            builder.ApplyConfiguration(new ResourceConfiguration());
        }
    }
}
