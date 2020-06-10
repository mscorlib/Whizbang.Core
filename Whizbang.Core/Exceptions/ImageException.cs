using System;
using System.Runtime.Serialization;

namespace Whizbang.Core.Exceptions
{
    [Serializable]
    public class ImageException : CustomException
    {
        public ImageException()
        {
        }

        public ImageException(string message)
            : base(message)
        {
        }

        public ImageException(string format, params object[] args)
            : base(string.Format(format, args))
        {
        }

        public ImageException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        public ImageException(string format, Exception innerException, params object[] args)
            : base(string.Format(format, args), innerException)
        {
        }

        protected ImageException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}