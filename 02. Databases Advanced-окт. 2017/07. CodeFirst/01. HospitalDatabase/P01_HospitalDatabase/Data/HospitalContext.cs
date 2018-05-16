namespace P01_HospitalDatabase.Data
{
    using Microsoft.EntityFrameworkCore;
    using P01_HospitalDatabase.Data.Models;

    public class HospitalContext : DbContext
    {
        public HospitalContext()
        {

        }

        public HospitalContext(DbContextOptions options)
            : base(options)
        {

        }

        public DbSet<Patient> Patients { get; set; }
        public DbSet<Visitation> Visitations { get; set; }
        public DbSet<Diagnose> Diagnoses { get; set; }
        public DbSet<Medicament> Medicaments { get; set; }
        public DbSet<PatientMedicament> PatientMedicaments { get; set; }
        public DbSet<Doctor> Doctors { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            if (!builder.IsConfigured)
            {
                builder.UseSqlServer(Configuration.ConnectionString);
            }
        }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Patient>(entity =>
            {
                entity.HasKey(e => e.PatientId); //Add PatientId as primary key


                entity.Property(e => e.FirstName)
                               .IsRequired()
                               .IsUnicode(true) // firstName is accepts unicode
                               .HasMaxLength(50); //describe lenght of characters

                entity.Property(e => e.LastName)
                               .IsRequired()
                               .IsUnicode(true)
                               .HasMaxLength(50);

                entity.Property(e => e.Address)
                               .IsRequired()
                               .IsUnicode(true)
                               .HasMaxLength(250);

                entity.Property(e => e.Email)
                               .IsRequired()
                               .IsUnicode(false)
                               .HasMaxLength(80);

                entity.Property(e => e.HasInsurance)
                               .HasDefaultValue(true); // add default value which is true.

            });

            builder.Entity<Visitation>(entity =>
            {
                entity.HasKey(e => e.VisitationId);

                entity.Property(e => e.Date)
                    .IsRequired()
                    .HasColumnType("DATETIME2") //determine type of column
                    .HasDefaultValueSql("GETDATE()"); // add date as I write query in sqlServer.Take datetime when in added in base.

                entity.Property(e => e.Comments)
                    .IsRequired(false)
                    .IsUnicode()
                    .HasMaxLength(250);

                entity.HasOne(e => e.Patient) // Visitation is with one patient.I need to add object Patient
                    .WithMany(p => p.Visitations) // Patient can has many visitations and I add colection of Visitations
                    .HasForeignKey(e => e.PatientId) //point toward PatientId
                    .HasConstraintName("FK_Visitation_Patient"); //If i like can name the constraint

                entity.Property(e => e.DoctorId)
                    .IsRequired(false);

                entity.HasOne(e => e.Doctor)
                    .WithMany(e => e.Visitations)
                    .HasForeignKey(e => e.DoctorId);
            });

            builder.Entity<Diagnose>(entity =>
            {
                entity.HasKey(e => e.DiagnoseId);

                entity.Property(e => e.Name)
                   .IsRequired()
                   .HasMaxLength(50);

                entity.Property(e => e.Comments)
                    .IsRequired(false)
                    .IsUnicode()
                    .HasMaxLength(250);

                entity.HasOne(e => e.Patient)
                    .WithMany(p => p.Diagnoses)
                    .HasForeignKey(e => e.PatientId);
            });

            builder.Entity<Medicament>(entity =>
            {
                entity.HasKey(e => e.MedicamentId);

                entity.Property(e => e.Name)
                    .IsRequired(true)
                    .IsUnicode(true)
                    .HasMaxLength(50);
            });

            builder.Entity<PatientMedicament>(entity =>
            {
                entity.HasKey(e => new { e.PatientId, e.MedicamentId }); //Add as keys PatientId and MedicamentId

                entity.HasOne(e => e.Medicament) //each medicament
                    .WithMany(e => e.Prescriptions) //has many prescriptions
                    .HasForeignKey(e => e.MedicamentId); //points to medicamentId

                entity.HasOne(e => e.Patient) //each patient
                    .WithMany(e => e.Prescriptions) // can has many prescriptions
                    .HasForeignKey(e => e.PatientId);//points to patientId
            });

            builder.Entity<Doctor>(entity =>
            {
                entity.HasKey(e => e.DoctorId);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .IsUnicode(true)
                    .HasMaxLength(100);

                entity.Property(e => e.Specialty)
                    .IsRequired()
                    .IsUnicode(true)
                    .HasMaxLength(100);

                
            });
        }

    }
}
