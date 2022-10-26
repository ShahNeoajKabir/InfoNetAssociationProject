using InfonetAssociates.DAL.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfonetAssociates.DAL.DatabaseContexts
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {
            //Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.EnableSensitiveDataLogging();
        }

        public DbSet<City> City { get; set; }
        public DbSet<Country> Country { get; set; }
        public DbSet<Language> Language { get; set; }
        public DbSet<MemberPersonalInformation> MemberPersonalInformation { get; set; }
        public DbSet<MemberPersonLanguage> MemberPersonLanguage { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
            modelBuilder.Entity<Country>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.CountryName);

               
            });
            modelBuilder.Entity<City>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.CityName);

                entity.HasOne(p => p.Country)
               .WithMany(d => d.City)
               .HasForeignKey(p => p.CountryId)
               .OnDelete(DeleteBehavior.Cascade);
            });
            modelBuilder.Entity<Language>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.LanguageName);
            });
            modelBuilder.Entity<MemberPersonalInformation>(entity =>
            {
                entity.HasKey(e => e.Id);
            });

            modelBuilder.Entity<MemberPersonLanguage>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.HasOne(p => p.Language)
               .WithMany(d => d.MemberPersonLanguage)
               .HasForeignKey(p => p.LanguageId)
               .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(p => p.MemberPersonalInformation)
            .WithMany(d => d.MemberPersonLanguage)
            .HasForeignKey(p => p.MemberPersonInfoId)
            .OnDelete(DeleteBehavior.Cascade);
            });
        }
    }
}
