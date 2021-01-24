using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Libre.Models;
using Microsoft.AspNetCore.Identity;

namespace Libre.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
            Database.Migrate();
        }
        public DbSet<IdentityUser> ApplicationUser { get; set; }
        public DbSet<Libre.Models.Book> Book { get; set; }
        public DbSet<Libre.Models.Genre> Genre { get; set; }

        /*
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Genre>()
                   .HasData(
                    new Genre { Id = Guid.NewGuid(), Name = "Fantastyka" },
                    new Genre { Id = Guid.NewGuid(), Name = "Historyczna" },
                    new Genre { Id = Guid.NewGuid(), Name = "Dokumentalna" },
                    new Genre { Id = Guid.NewGuid(), Name = "Science fiction" },
                    new Genre { Id = Guid.NewGuid(), Name = "Horror" },
                    new Genre { Id = Guid.NewGuid(), Name = "Romans" }
                );

            builder.Entity<Genre>()
                 .HasData(
                  new Genre { Id = Guid.NewGuid(), Name = "Fantastyka" },
                  new Genre { Id = Guid.NewGuid(), Name = "Historyczna" },
                  new Genre { Id = Guid.NewGuid(), Name = "Dokumentalna" },
                  new Genre { Id = Guid.NewGuid(), Name = "Science fiction" },
                  new Genre { Id = Guid.NewGuid(), Name = "Horror" },
                  new Genre { Id = Guid.NewGuid(), Name = "Romans" }
              );
        }*/
    }
}
