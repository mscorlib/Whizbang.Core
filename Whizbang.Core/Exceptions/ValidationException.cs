using System;
using System.Runtime.Serialization;

namespace Whizbang.Core.Exceptions
{
    /// <summary>
    ///     验证异常
    /// </summary>
    [Serializable]
    public class ValidationException : CustomException
    {
        public ValidationException()
        {
        }

        public ValidationException(string message)
            : base(message)
        {
        }

        public ValidationException(string format, params object[] args)
            : base(string.Format(format, args))
        {
        }

        public ValidationException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        public ValidationException(string format, Exception innerException, params object[] args)
            : base(string.Format(format, args), innerException)
        {
        }

        protected ValidationException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}