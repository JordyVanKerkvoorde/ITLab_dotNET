using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ITLab29.Exceptions
{
    public class AlreadyOpenException : Exception
    {
        public AlreadyOpenException(string message) : base(message)
        {

        }
    }
}
