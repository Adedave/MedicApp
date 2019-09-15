using MedicApp.Api.Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedicApp.Api.Persistence.Contexts
{
    public class MedicAppDbContext : DbContext
    {
        public MedicAppDbContext(DbContextOptions<MedicAppDbContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Enrollee>().HasMany(
                x => x.Diseases);
            modelBuilder.Entity<Hospital>().HasMany(
                x => x.Enrollees);


            modelBuilder.Entity<Location>().HasMany(
                x => x.Hospitals);

            modelBuilder.Entity<Disease>().HasMany(
                x => x.Enrollees);

            modelBuilder.Entity<EnrolleeDisease>().HasKey(
                x => new { x.EnrolleeId, x.DiseaseId}
                );
        }

        public DbSet<Enrollee> Enrollees { get; set; }
        public DbSet<Hospital> Hospitals { get; set; }
        public DbSet<Location> Locations { get; set; }

        public DbSet<Disease> Diseases { get; set; }
        public DbSet<EnrolleeDisease> EnrolleeDiseases { get; set; }

    }
}
