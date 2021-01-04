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
    }
}
