using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FI.Utils.Exceptions
{

    [Serializable]
    public class CpfJaExisteException : Exception
    {
        public CpfJaExisteException() { }
        public CpfJaExisteException(string message) : base(message) { }
        public CpfJaExisteException(string message, Exception inner) : base(message, inner) { }
        protected CpfJaExisteException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
