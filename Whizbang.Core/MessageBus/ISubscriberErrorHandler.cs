using System;

namespace Whizbang.Core.MessageBus
{
    /// <summary>
    ///     错误处理器接口
    /// </summary>
    public interface ISubscriberErrorHandler
    {
        void Handle(IMessage message, Exception ex);
    }
}