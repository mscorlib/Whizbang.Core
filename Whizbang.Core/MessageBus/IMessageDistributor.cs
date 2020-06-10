namespace Whizbang.Core.MessageBus
{
    /// <summary>
    ///     消息订阅器接口
    /// </summary>
    public interface IMessageDistributor
    {
        //Guid DebugKey { get; set; }

        /// <summary>
        ///     注册消息Handler
        /// </summary>
        /// <typeparam name="TMessage">消息类型</typeparam>
        /// <param name="handler">消息处理器</param>
        void Register<TMessage>(IHandler<TMessage> handler) where TMessage : class, IMessage;

        /// <summary>
        ///     发布消息
        /// </summary>
        /// <typeparam name="TMessage">消息类型</typeparam>
        /// <param name="message">消息</param>
        void Distribute<TMessage>(TMessage message) where TMessage : class, IMessage;

        /// <summary>
        ///     退订消息
        /// </summary>
        /// <typeparam name="TMessage">消息类型</typeparam>
        /// <param name="handler">消息处理器</param>
        void UnSubcribe<TMessage>(IHandler<TMessage> handler) where TMessage : class, IMessage;
    }
}