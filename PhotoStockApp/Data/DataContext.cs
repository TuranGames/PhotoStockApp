using Microsoft.EntityFrameworkCore;
using PhotoStockApp.Models;

namespace PhotoStockApp.Data
{
    public class DataContext : DbContext
    {
        public DataContext()
        {
        }

        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }


        public DbSet<Author> Authors { get; set; }
        public DbSet<Photo> Photos { get; set; }
        public DbSet<Text> Texts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Photo>()
                    .HasOne(p => p.Author);
            modelBuilder.Entity<Text>()
                    .HasOne(p => p.Author);
        }

        }
}
