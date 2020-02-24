using ITLab29.Models.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ITLab29.Data.Repositories
{
    public class LocationRepository : ILocationRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly DbSet<Location> _locations;

        public LocationRepository(ApplicationDbContext context)
        {
            _context = context;
            _locations = context.Locations;
        }
        public IEnumerable<Location> GetAll()
        {
            return _locations.AsNoTracking().ToList();
        }

        public IEnumerable<Location> GetByCampus(CampusEnum campus)
        {
            return _locations.Where(l => l.Campus == campus);
        }

        public Location GetById(string locationId)
        {
            return _locations.SingleOrDefault(l => l.LocationId == locationId);
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}
