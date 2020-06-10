using System;

namespace Whizbang.Core.EventSource
{
    /// <summary>
    ///     事件接口
    /// </summary>
    public interface IEvent : IMessage
    {
        /// <summary>
        ///     时间戳
        /// </summary>
        DateTime Timestamp { get; }
    }
}