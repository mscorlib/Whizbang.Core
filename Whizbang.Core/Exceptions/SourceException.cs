using System;
using System.Runtime.Serialization;

namespace Whizbang.Core.Exceptions
{
    /// <summary>
    ///     源异常
    /// </summary>
    [Serializable]
    public class SourceException : CustomException
    {
        public SourceException()
        {
        }

        public SourceException(string message)
            : base(message)
        {
        }

        public SourceException(string format, params object[] args)
            : base(string.Format(format, args))
        {
        }

        public SourceException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        public SourceException(string format, Exception innerException, params object[] args)
            : base(string.Format(format, args), innerException)
        {
        }

        protected SourceException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}