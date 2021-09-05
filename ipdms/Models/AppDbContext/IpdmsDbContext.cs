using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ipdms.Models.AppDbContext
{
    public class IpdmsDbContext : DbContext
    {
        public DbSet<Movie> Movies { get; set; }
        public DbSet<IpdmsUser> IpdmsUser { get; set; }

        public IpdmsDbContext(DbContextOptions<IpdmsDbContext> options) : base(options){}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<Movie>().ToTable("Movie");
            modelBuilder.Entity<IpdmsUser>().ToTable("IpdmsUser");
        }
    }
}
