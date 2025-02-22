﻿using System;
using System.Collections.Generic;
using System.Text;
using ITLab29.Data.Mapping;
using ITLab29.Models.Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ITLab29.Data {
    public class ApplicationDbContext : IdentityDbContext<User> {
        public DbSet<User> Users { get; set; }
        public DbSet<Media> Media { get; set; }
        public DbSet<Session> Sessions { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<Guest> Guests { get; set; }
        public DbSet<Feedback> Feedback { get; set; }
        public DbSet<UserSession> UserSessions { get; set; }
        public DbSet<Announcement> Announcements { get; set;}

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfiguration(new SessionConfiguration());
            builder.ApplyConfiguration(new UserSessionConfiguration());
            builder.ApplyConfiguration(new LocationConfiguration());
            builder.ApplyConfiguration(new MediaMapper());
            builder.ApplyConfiguration(new GuestConfiguration());
            builder.Entity<UserSession>().HasKey(t => new { t.SessionId, t.UserId });
            builder.Entity<UserSession>()
                .HasOne(pt => pt.User).WithMany(p => p.UserSessions)
                .HasForeignKey(pt => pt.UserId);
            builder.Entity<UserSession>()
                .HasOne(pt => pt.Session).WithMany(p => p.UserSessions)
                .HasForeignKey(pt => pt.SessionId);

        }
    }
}
