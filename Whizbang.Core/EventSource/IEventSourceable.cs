using System.Collections.Generic;

namespace Whizbang.Core.EventSource
{
    /// <summary>
    ///     可溯源对象
    /// </summary>
    public interface IEventSourceable
    {
        /// <summary>
        ///     未提交的领域事件
        /// </summary>
        IEnumerable<IDomainEvent> UncommittedEvents { get; }

        /// <summary>
        ///     从历史事件构建对象
        /// </summary>
        /// <param name="historicalEvents"></param>
        void BuildFromEvents(IEnumerable<IDomainEvent> historicalEvents);
    }
}