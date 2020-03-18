﻿using ITLab29.Models.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ITLab29.Data.Repositories {
    public class AnnouncementRepository : IAnnouncementRepository {
        private readonly ApplicationDbContext _context;
        private readonly DbSet<Announcement> _announcements;

        public AnnouncementRepository(ApplicationDbContext context) {
            _context = context;
            _announcements = context.Announcements;
        }

        public IEnumerable<Announcement> GetAll() {
            return _announcements.ToList();
        }

        public Announcement GetById(int id) {
            return _announcements.Where(a => a.Id == id).FirstOrDefault();
        }

        public void SaveChanges() {
            _context.SaveChanges();
        }
    }
}
