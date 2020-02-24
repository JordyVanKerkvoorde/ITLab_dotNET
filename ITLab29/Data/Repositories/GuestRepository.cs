using ITLab29.Models.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ITLab29.Data.Repositories
{
    public class GuestRepository : IGuestRepository
    {

        private readonly ApplicationDbContext _context;
        private readonly DbSet<Guest> _guests;

        public GuestRepository(ApplicationDbContext context)
        {
            _context = context;
            _guests = context.Guests;

        }
        public IEnumerable<Guest> GetAll()
        {
            return _guests.AsNoTracking().ToList();
        }

        public Guest GetById(int guestId)
        {
            return _guests.SingleOrDefault(g => g.GuestId == guestId);
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}
