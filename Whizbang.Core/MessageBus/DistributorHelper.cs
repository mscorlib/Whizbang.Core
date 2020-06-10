using System;
using System.Linq;
using System.Reflection;

namespace Whizbang.Core.MessageBus
{
    public static class DistributorHelper
    {
        /// <summary>
        ///     注册Handler
        /// </summary>
        /// <param name="distributor">消息分发器</param>
        /// <param name="handlerType">Handler类型</param>
        public static void RegisterType(IMessageDistributor distributor, Type handlerType)
        {
            MethodInfo registerInfo = distributor.GetType().GetMethod("Register", BindingFlags.Public | BindingFlags.Instance);

            //单个Type，继承多个IHanderl<T>
            var handlers = from p in handlerType.GetInterfaces()
                           where p.IsGenericType &&
                                 p.GetGenericTypeDefinition() == typeof(IHandler<>)
                           select p;

            foreach (var handler in handlers)
            {
                var handlerInstance = Activator.CreateInstance(handlerType);
                var tMessage = handler.GetGenericArguments().First();
                var methodInfo = registerInfo.MakeGenericMethod(tMessage);
                methodInfo.Invoke(distributor, new[] { handlerInstance });
            }
        }
    }
}