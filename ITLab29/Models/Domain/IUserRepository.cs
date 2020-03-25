using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ITLab29.Models.Domain
{
    public interface IUserRepository
    {
        IEnumerable<User> GetAll();
        User GetById(string userId);
        IEnumerable<User> GetByLastName(string lastName);
        User GetByName(string name);
        IEnumerable<Session> GetRegisteredSessions(string userId);
        void SaveChanges();
    }
}
