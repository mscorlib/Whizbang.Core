using System;
using System.Runtime.Serialization;

namespace Whizbang.Core.Exceptions
{
    /// <summary>
    ///     对象未找到异常
    /// </summary>
    [Serializable]
    public class NotFoundException : CustomException
    {
        public NotFoundException()
        {
        }

        public NotFoundException(string message)
            : base(message)
        {
        }

        public NotFoundException(string format, params object[] args)
            : base(string.Format(format, args))
        {
        }

        public NotFoundException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        public NotFoundException(string format, Exception innerException, params object[] args)
            : base(string.Format(format, args), innerException)
        {
        }

        protected NotFoundException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}