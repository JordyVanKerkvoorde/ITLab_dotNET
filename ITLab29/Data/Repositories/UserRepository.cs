using ITLab29.Models.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ITLab29.Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly DbSet<User> _users;

        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
            _users = context.Users;
        }

        public IEnumerable<User> GetAll()
        {
            return _users.AsNoTracking().ToList();
        }

        public User GetById(string userId)
        {
            return _users
                //.Include(u => u.UserSessions)
                //.Include(u => u.UserStatus)
                //.Include(u => u.UserType)
                .Include(u => u.Avatar)
                .SingleOrDefault(u => u.Id == userId);
        }

        public IEnumerable<User> GetByLastName(string lastName)
        {
            return _users.Where(u => u.LastName == lastName).ToList();
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}
