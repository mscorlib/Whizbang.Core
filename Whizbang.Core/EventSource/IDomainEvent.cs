using System;

namespace Whizbang.Core.EventSource
{
    /// <summary>
    ///     领域事件接口
    /// </summary>
    public interface IDomainEvent : IEvent
    {
        /// <summary>
        ///     事件源Id
        /// </summary>
        Guid SourceId { get; }

        /// <summary>
        ///     版本
        /// </summary>
        int Version { get; set; }
    }
}