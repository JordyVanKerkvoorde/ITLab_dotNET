using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ITLab29.Models.Domain
{
    interface ILocationRepository
    {
        IEnumerable<Location> GetAll();
        Location GetById(string locationId);
        IEnumerable<Location> GetByCampus(CampusEnum campus);
        void SaveChanges();
    }
}
