namespace P03_SalesDatabase
{
    using Microsoft.EntityFrameworkCore;
    using P03_SalesDatabase.Data.Models;

    public class SalesContext : DbContext
    {
        public SalesContext()
        {

        }

        public SalesContext(DbContextOptions options)
            :base(options)
        {

        }

        public DbSet<Sale> Sales { get; set; }
        public DbSet<Store> Stores { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Customer> Customers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            if (!builder.IsConfigured)
            {
                builder.UseSqlServer(Configuration.ConnectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Sale>(entity =>
            {
                entity.HasKey(e => e.SaleId);

                entity.Property(e => e.Date)
                    .IsRequired(true)
                    .HasColumnType("DATETIME2")
                    .HasDefaultValueSql("GETDATE()");

                entity.HasOne(e => e.Product)
                    .WithMany(s => s.Sales)
                    .HasForeignKey(e => e.ProductId);

                entity.Property(e => e.CustomerId)
                    .IsRequired(true);

                entity.HasOne(e => e.Customer)
                    .WithMany(c => c.Sales)
                    .HasForeignKey(e => e.CustomerId);

                entity.HasOne(e => e.Store)
                    .WithMany(s => s.Sales)
                    .HasForeignKey(e => e.StoreId);
            });

            builder.Entity<Store>(entity =>
            {
                entity.HasKey(e => e.StoreId);

                entity.Property(e => e.Name)
                   .IsRequired()
                   .IsUnicode(true)
                   .HasMaxLength(80);
            });

            builder.Entity<Product>(entity =>
            {
                entity.HasKey(e => e.ProductId);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .IsUnicode(true)
                    .HasMaxLength(50);

                entity.Property(e => e.Quantity)
                    .IsRequired();

                entity.Property(e => e.Price)
                    .IsRequired();

                entity.Property(e => e.Description)
                    .IsRequired(true)
                    .IsUnicode(true)
                    .HasMaxLength(250)
                    .HasDefaultValue("No description");
            });

            builder.Entity<Customer>(entity =>
            {
                entity.HasKey(e => e.CustomerId);

                entity.Property(e => e.Name)
                    .HasMaxLength(100)
                    .IsUnicode(true)
                    .IsRequired(true);

                entity.Property(e => e.Email)
                    .HasMaxLength(80)
                    .IsUnicode(false)
                    .IsRequired(true);

                entity.Property(e => e.CreditCardNumber)
                    .IsRequired();
            });
        }
    }
}
