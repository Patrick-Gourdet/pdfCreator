using System;
using System.Runtime.Serialization;

namespace GridSystems.ElderScroll.Common
{
    public class EsGeneratorException : Exception
    {
        public EsGeneratorException() : base("Error Generating PDF")
        {
        }

        public EsGeneratorException(string message) : base(message)
        {
        }

        public EsGeneratorException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected EsGeneratorException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
