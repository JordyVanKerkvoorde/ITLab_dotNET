using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ITLab29.Models.Domain
{
    public interface IUserSessionRepository
    {
        void AddUsertoSession(User user, Session session);

        void AddSessiontoUser(Session session, User user);

        IEnumerable<User> GetUsersBySessionId(int sessionId);

        IEnumerable<Session> GetSessionsByUser(string userId);

        void SaveChanges();
    }
}
