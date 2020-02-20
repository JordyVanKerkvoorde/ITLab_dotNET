using System;
using System.Collections.Generic;
using System.Text;
using ITLab29.Models.Domain;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ITLab29.Data {
    public class ApplicationDbContext : IdentityDbContext {
        //public DbSet<User> Users { get; set; }
        //public DbSet<Media> Media { get; set; }
        //public DbSet<Session> Sessions { get; set; }
        //public DbSet<Location> Locations { get; set; }
        public DbSet<Guest> Guests { get; set; }
        //public DbSet<Feedback> Feedback { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) {
        }
    }
}
