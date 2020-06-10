using System;
using System.Runtime.Serialization;

namespace Whizbang.Core.Exceptions
{
    /// <summary>
    ///     自定义异常基类
    /// </summary>
    public abstract class CustomException : ApplicationException
    {
        protected CustomException()
        {
        }

        protected CustomException(string message)
            : base(message)
        {
        }

        protected CustomException(string format, params object[] args)
            : base(string.Format(format, args))
        {
        }

        protected CustomException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        protected CustomException(string format, Exception innerException, params object[] args)
            : base(string.Format(format, args), innerException)
        {
        }

        protected CustomException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}