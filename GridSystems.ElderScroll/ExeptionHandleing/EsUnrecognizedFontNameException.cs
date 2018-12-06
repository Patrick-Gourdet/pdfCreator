using System;
using System.Runtime.Serialization;

namespace GridSystems.ElderScroll.Common
{
    public class EsUnrecognizedFontNameException : EsGeneratorException
    {
        public EsUnrecognizedFontNameException(string fontName) : this(fontName, null)
        {
        }

        public EsUnrecognizedFontNameException(string fontName, Exception innerException) : base("Unrecognized Font Name '" + fontName ?? "" + "'", innerException)
        {
            this.FontName = fontName;
        }

        protected EsUnrecognizedFontNameException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public string FontName { get; private set; }
    }
}
