using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace SimpleNMA
{
    public class SimpleNMAException : Exception
    {
        public SimpleNMAException()
        {
        }

        public SimpleNMAException(string message) : base(message)
        {
        }

        public SimpleNMAException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected SimpleNMAException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
