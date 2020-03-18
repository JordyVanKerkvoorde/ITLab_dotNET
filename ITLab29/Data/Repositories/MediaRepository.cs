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
        private readonly DbSet<User> _users;

        public MediaRepository(ApplicationDbContext context)
        {
            _context = context;
            _media = context.Media;
            _users = context.Users;
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
        public Media GetAvatar(string userId)
        {
            User user = _users.Include(u => u.Avatar).ToList().FirstOrDefault();
            if (user == null)
            {
                return new Media(MediaType.IMAGE, "/photo/800x560.png");
            }
            else
            {
                return user.Avatar;
            }

        }
        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}
