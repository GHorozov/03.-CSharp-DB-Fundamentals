namespace BookShop.Data
{
    using Microsoft.EntityFrameworkCore;
    using BookShop.Models;
    using BookShop.Data.EntriesConfiguration;

    public class BookShopContext : DbContext
    {

        public BookShopContext()
        {

        }

        public BookShopContext(DbContextOptions options)
            : base(options)
        {

        }

        //Db sets
        public DbSet<Author> Authors { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<BookCategory> BookCategories { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(ConfiguringConnection.ConsigurationString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ConfigurationAuthor());
            modelBuilder.ApplyConfiguration(new ConfigurationBook());
            modelBuilder.ApplyConfiguration(new ConfigurationCategory());
            modelBuilder.ApplyConfiguration(new ConfigurationBookCategory());
        }
    }
}
