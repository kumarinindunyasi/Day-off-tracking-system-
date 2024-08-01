using Microsoft.EntityFrameworkCore;
using API.Entities;
using API.EntitiyConfigs;

namespace API.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
          : base(options)
        { }
        public DbSet<Person> Persons { get; set; }
        public DbSet<Off> Offs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new PersonConfig());       
        }


    }
}
