using System;
using System.Runtime.Serialization;
using System.Text;
namespace GridSystems.ElderScroll.Common
{
    class EsConflictingParameterException : EsGeneratorException
    {
        public EsConflictingParameterException(string conflictMessage,params string[] parameterNames) : this(conflictMessage, null,  parameterNames)
        {
        }

        public EsConflictingParameterException(string conflictMessage,  Exception innerException, params string[] parameterNames) : base(GetMessege(parameterNames, conflictMessage), innerException)
        {
            this.ParameterNames = parameterNames;
            this.ConflictMessage = conflictMessage;
        }

        protected EsConflictingParameterException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
        public string[] ParameterNames { get; private set; }
        public string ConflictMessage { get; private set; }
        private static string GetMessege(string[] parameterNames, string conflictMessage)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("There is a conflict between following parameters ");
            foreach (string parameterName in parameterNames)
            {
                sb.Append(parameterName).Append(",");
            }
            sb.Length--;
            if (!string.IsNullOrEmpty(conflictMessage))
            {
                sb.Append(" Because ").Append(conflictMessage);
            }
            return sb.ToString();
        }
    }
}
