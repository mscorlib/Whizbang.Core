using System.Collections.Generic;

namespace Whizbang.Core.MessageBus
{
    public interface IBus
    {
        /// <summary>
        ///     发布消息
        /// </summary>
        /// <typeparam name="TMessage">消息类型</typeparam>
        /// <param name="message">消息</param>
        void Publish<TMessage>(TMessage message) where TMessage : class, IMessage;

        /// <summary>
        ///     发布消息
        /// </summary>
        /// <typeparam name="TMessage">消息类型</typeparam>
        /// <param name="message">消息</param>
        void Publish<TMessage>(IEnumerable<TMessage> message) where TMessage : class, IMessage;
    }
}