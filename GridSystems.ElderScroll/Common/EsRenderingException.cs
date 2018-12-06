using System;
using System.Runtime.Serialization;

namespace GridSystems.ElderScroll.Common
{
    public class EsRenderingException : EsGeneratorException
    {
        public EsRenderingException(IEsElement esElement) : this(esElement, null)
        {
        }

        public EsRenderingException(IEsElement esElement, Exception innerException) : this(string.Concat("Error rendering '", esElement?.GetType().Name ?? "", "'"), esElement, innerException)
        {
        }

        public EsRenderingException(string message, IEsElement esElement) : this(message, esElement, null)
        {
        }

        public EsRenderingException(string message, IEsElement esElement, Exception innerException) : base(message, innerException)
        {
            this.EsElement = esElement;
        }

        protected EsRenderingException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public IEsElement EsElement { get; private set; }
    }
}
