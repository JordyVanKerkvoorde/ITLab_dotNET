using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ITLab29.Models.Domain
{
    public class Location
    {

        public string LocationId { get; }
        public LocationEnum.Campus Campus { get; }
        public int Capacity { get; set; }

        public Location(string locationId, LocationEnum.Campus campus, int capacity)
        {
            LocationId = locationId;
            Campus = campus;
            Capacity = capacity;
        }


    }
}
