using Fithub.Database.Models;
using Microsoft.EntityFrameworkCore;

namespace Fithub.Database
{
    public class FithubDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public DbSet<Category> Categories { get; set; }

        public DbSet<Exercise> Exercises { get; set; }

        public FithubDbContext(DbContextOptions<FithubDbContext> options) : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(FithubDbContext).Assembly);
        }
    }
}
