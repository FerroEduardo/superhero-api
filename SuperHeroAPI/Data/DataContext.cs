using Microsoft.EntityFrameworkCore;
using SuperHeroAPI.Models;

namespace SuperHeroAPI.Data
{
    public class DataContext : DbContext
    {
        private readonly IConfiguration config;

        public DataContext(DbContextOptions<DataContext> options, IConfiguration config) : base (options)
        {
            this.config = config;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer(config["Database:URL"]);
        }

        public DbSet<SuperHero> SuperHeroes { get; set; }

        public DbSet<User> User { get; set; }
    }
}
