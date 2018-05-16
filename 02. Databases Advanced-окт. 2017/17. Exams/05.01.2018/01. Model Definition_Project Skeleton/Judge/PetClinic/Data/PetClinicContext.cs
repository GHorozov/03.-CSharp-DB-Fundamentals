namespace PetClinic.Data
{
    using Microsoft.EntityFrameworkCore;
    using PetClinic.Models;

    public class PetClinicContext : DbContext
    {
        public PetClinicContext() { }

        public PetClinicContext(DbContextOptions options)
            :base(options) { }


        //DbSets
        public DbSet<Animal> Animals { get; set; }
        public DbSet<Passport> Passports { get; set; }
        public DbSet<Vet> Vets { get; set; }
        public DbSet<Procedure> Procedures { get; set; }
        public DbSet<AnimalAid> AnimalAids { get; set; }
        public DbSet<ProcedureAnimalAid> ProceduresAnimalAids { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(Configuration.ConnectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            //animals
            builder.Entity<Animal>()
                .HasKey(e => e.Id);

            builder.Entity<Animal>()
                .Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(20);

            builder.Entity<Animal>()
               .Property(e => e.Type)
               .IsRequired()
               .HasMaxLength(20);

            //Passport
            builder.Entity<Passport>()
                .HasKey(e => e.SerialNumber);

            //Vet
            builder.Entity<Vet>()
                .HasAlternateKey(e => e.PhoneNumber);

            //AnimakAid
            builder.Entity<AnimalAid>()
                .HasAlternateKey(e => e.Name);

            //ProcedureAnimalAid
            builder.Entity<ProcedureAnimalAid>()
                .HasKey(e => new { e.AnimalAidId, e.ProcedureId });

            builder.Entity<ProcedureAnimalAid>()
               .HasOne(e => e.AnimalAid)
               .WithMany(p => p.AnimalAidProcedures)
               .HasForeignKey(e => e.AnimalAidId);

            builder.Entity<ProcedureAnimalAid>()
                .HasOne(e => e.Procedure)
                .WithMany(p => p.ProcedureAnimalAids)
                .HasForeignKey(e => e.ProcedureId);

        }
    }
}
