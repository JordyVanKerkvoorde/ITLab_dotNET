using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ITLab29.Models.Domain
{
    public class Media
    {

        public int MediaId { get; }
        public MediaEnum Type { get; }
        public string Path { get; set; }

        public Media(MediaEnum type, string path)
        {
            Type = type;
            Path = path;
        }

    }
}
