using Microsoft.EntityFrameworkCore;
using API.Entities;
using System.Collections.Generic;

namespace CORE.Context
{
    public class Contexts : DbContext
    {
        public Contexts(DbContextOptions<Contexts> options)
          : base(options)
        { }
        public DbSet<Person> Persons { get; set; }
        public DbSet<Izin> Izinler { get; set; }




    }
}
