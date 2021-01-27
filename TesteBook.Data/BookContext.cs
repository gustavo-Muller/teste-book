using Microsoft.EntityFrameworkCore;
using System.Reflection;
using TesteBook.Business.Model;

namespace TesteBook.Data
{
    public class BookContext : DbContext
    {
        public DbSet<Volume> Volumes { get; set; }

        public BookContext()
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("FileName=Book.db", options =>
            {
                options.MigrationsAssembly(Assembly.GetExecutingAssembly().FullName);
            });

            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Volume>().ToTable("Volumes", "teste");
            modelBuilder.Entity<Volume>(entity => 
            {
                entity.HasKey(e => e.Id);
                entity.HasIndex(e => e.Title);
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}
