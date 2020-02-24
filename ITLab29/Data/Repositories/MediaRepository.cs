using ITLab29.Models.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ITLab29.Data.Repositories
{
    public class MediaRepository : IMediaRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly DbSet<Media> _media;

        public MediaRepository(ApplicationDbContext context)
        {
            _context = context;
            _media = context.Media;
        }

        public IEnumerable<Media> GetAll()
        {
            return _media.AsNoTracking().ToList();
        }

        public Media GetById(int mediaId)
        {
            return _media.SingleOrDefault(m => m.MediaId == mediaId);
        }

        public IEnumerable<Media> GetByType(MediaType type)
        {
            return _media.Where(m => m.Type == type);
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}
