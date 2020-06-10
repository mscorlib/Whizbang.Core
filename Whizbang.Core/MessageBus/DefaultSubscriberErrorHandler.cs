using System;

namespace Whizbang.Core.MessageBus
{
    /// <summary>
    ///     错误处理器
    /// </summary>
    public class DefaultSubscriberErrorHandler : ISubscriberErrorHandler
    {
        public void Handle(IMessage message, Exception ex)
        {
            //--todo log error
        }
    }
}