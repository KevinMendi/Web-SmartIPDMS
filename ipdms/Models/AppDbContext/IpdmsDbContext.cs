using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ipdms.Models;

namespace ipdms.Models.AppDbContext
{
    public class IpdmsDbContext : DbContext
    {
        public DbSet<Movie> Movies { get; set; }
        public DbSet<IpdmsUser> IpdmsUser { get; set; }

        public DbSet<Project> Project { get; set; }

        public DbSet<Document> Document { get; set; }

        public DbSet<ipdms.Models.ApplicationType> ApplicationType { get; set; }

        public IpdmsDbContext(DbContextOptions<IpdmsDbContext> options) : base(options){}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<Movie>().ToTable("Movie");
            modelBuilder.Entity<IpdmsUser>().ToTable("IpdmsUser");
            modelBuilder.Entity<Project>().ToTable("Project");
            modelBuilder.Entity<Document>().ToTable("Document");
            modelBuilder.Entity<ApplicationType>().ToTable("lk_ApplicationType");
        }

        

      
    }
}
