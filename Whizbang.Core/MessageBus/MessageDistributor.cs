using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Whizbang.Core.MessageBus
{
    /// <summary>
    ///     消息订阅器实现
    /// </summary>
    public class MessageDistributor : IMessageDistributor
    {
        public MessageDistributor()
        {
            _subscriberErrorHandler = new DefaultSubscriberErrorHandler();
            //DebugKey = Guid.NewGuid();
        }

        #region IMessageBus Members

        //public Guid DebugKey { get; set; }//测试单例

        /// <summary>
        ///     注册消息Handler
        /// </summary>
        /// <typeparam name="TMessage"></typeparam>
        /// <param name="handler"></param>
        public void Register<TMessage>(IHandler<TMessage> handler) where TMessage : class, IMessage
        {
            Type keyType = typeof(TMessage);

            List<object> handlers;

            if (!_handlers.ContainsKey(keyType))
            {
                handlers = new List<object>();
                _handlers.TryAdd(keyType, handlers);
            }
            else
            {
                handlers = _handlers[keyType] ?? new List<object>();
            }

            if (handlers.All(x => x.GetType() != handler.GetType()))
                handlers.Add(handler);
        }

        /// <summary>
        ///     分发消息
        /// </summary>
        /// <typeparam name="TMessage">消息类型</typeparam>
        /// <param name="message">消息</param>
        public void Distribute<TMessage>(TMessage message) where TMessage : class, IMessage
        {
            if (message == null)
                throw new ArgumentNullException("message");

            Type type = message.GetType();

            if (!_handlers.ContainsKey(type)) return;

            List<object> handlers = _handlers[type];

            foreach (var handler in handlers)
            {
                var handlerMethod = getHandlerMethod(type);

                // --todo publish event
                try
                {
                    handlerMethod.Invoke(handler, new object[] { message });
                }
                catch (Exception ex)
                {
                    _subscriberErrorHandler.Handle(message, ex);
                }
            }
        }

        /// <summary>
        ///     退订消息
        /// </summary>
        /// <typeparam name="TMessage">消息类型</typeparam>
        /// <param name="handler">消息Handler</param>
        public void UnSubcribe<TMessage>(IHandler<TMessage> handler) where TMessage : class, IMessage
        {
            Type keyType = typeof(TMessage);

            if (_handlers.ContainsKey(keyType) &&
                _handlers[keyType] != null &&
                _handlers[keyType].Any(x => x.Equals(handler)))
            {
                _handlers[keyType].Remove(handler);
            }
        }

        #endregion IMessageBus Members

        #region private properties&fields&method

        /// <summary>
        ///     获取IHandler`1的handle方法元数据
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        private MethodInfo getHandlerMethod(Type type)
        {
            if (_handlersInfos.ContainsKey(type))
                return _handlersInfos[type];

            var methodInfo = typeof(IHandler<>).MakeGenericType(type).GetMethod("Handle");

            _handlersInfos.TryAdd(type, methodInfo);

            return methodInfo;
        }

        private readonly ConcurrentDictionary<Type, List<object>> _handlers = new ConcurrentDictionary<Type, List<object>>();//保存已注册的IHandler<>

        private readonly ConcurrentDictionary<Type, MethodInfo> _handlersInfos = new ConcurrentDictionary<Type, MethodInfo>();//缓存IHandler`1的handle方法元数据

        private readonly ISubscriberErrorHandler _subscriberErrorHandler; //订阅者错误Handler

        #endregion private properties&fields&method
    }
}