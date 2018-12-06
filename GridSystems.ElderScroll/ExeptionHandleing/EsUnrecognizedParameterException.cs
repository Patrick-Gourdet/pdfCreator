using System;
using System.Runtime.Serialization;

namespace GridSystems.ElderScroll.Common
{
    public class EsUnrecognizedParameterException : EsGeneratorException
    {
        public EsUnrecognizedParameterException(string parameterName, string parameterValue) : this(parameterName, parameterValue, null)
        {
        }

        public EsUnrecognizedParameterException(string parameterName, string parameterValue, Exception innerException) : base("Unrecognized value '" + parameterValue ?? "" + "' for parameter '" + parameterName + "'", innerException)
        {
            this.ParameterName = parameterName;
            this.ParameterValue = parameterValue;
        }

        protected EsUnrecognizedParameterException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public string ParameterName { get; private set; }
        public string ParameterValue { get; private set; }
    }
}
