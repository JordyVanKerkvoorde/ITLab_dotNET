using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ITLab29.Models.Domain
{
    public interface IGuestRepository
    {
        IEnumerable<Guest> GetAll();
        Guest GetById(int guestId);
        void SaveChanges();

    }
}
