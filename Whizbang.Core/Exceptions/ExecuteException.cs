using System;
using System.Runtime.Serialization;

namespace Whizbang.Core.Exceptions
{
    /// <summary>
    ///     执行异常
    /// </summary>
    [Serializable]
    public class ExecuteException : CustomException
    {
        public ExecuteException()
        {
        }

        public ExecuteException(string message)
            : base(message)
        {
        }

        public ExecuteException(string format, params object[] args)
            : base(string.Format(format, args))
        {
        }

        public ExecuteException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        public ExecuteException(string format, Exception innerException, params object[] args)
            : base(string.Format(format, args), innerException)
        {
        }

        protected ExecuteException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}