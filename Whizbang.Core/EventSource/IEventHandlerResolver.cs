using System.Collections.Generic;

namespace Whizbang.Core.EventSource
{
    /// <summary>
    ///     事件处理程序解析器
    /// </summary>
    public interface IEventHandlerResolver
    {
        /// <summary>
        ///     根据事件获取处理程序列表
        /// </summary>
        /// <typeparam name="TEvent">事件类型</typeparam>
        /// <returns></returns>
        IEnumerable<IEventHandler<TEvent>> Resolve<TEvent>() where TEvent : IEvent;
    }
}