using Microsoft.EntityFrameworkCore;
using SuperHeroAPI.Models;
using System.Reflection.Metadata;

namespace SuperHeroAPI.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base (options)
        {
            
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer("Server=localhost\\SQLEXPRESS;Database=superhero;Trusted_Connection=True;TrustServerCertificate=True;");
        }

        public DbSet<SuperHero> SuperHeroes { get; set; }

        public DbSet<User> User { get; set; }
    }
}
