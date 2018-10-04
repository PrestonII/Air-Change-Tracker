using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIVE.Domain.Exceptions
{
    public class CancellableException : Exception
    {
        public CancellableException() { }

        public CancellableException(string msg) : base(msg) { }

        public CancellableException(string msg, Exception inner) : base(msg, inner) { }
    }
}
