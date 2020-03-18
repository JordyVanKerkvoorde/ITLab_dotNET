using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ITLab29.Models.Domain {
    public interface IAnnouncementRepository {
        IEnumerable<Announcement> GetAll();
        Announcement GetById(int id);
    }
}
