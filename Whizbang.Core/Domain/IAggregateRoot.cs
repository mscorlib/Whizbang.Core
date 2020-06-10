using System;
using Whizbang.Core.EventSource;

namespace Whizbang.Core.Domain
{
    /// <summary>
    ///     基础聚合根
    /// </summary>
    /// <typeparam name="TKey">标识类型</typeparam>
    public interface IAggregateRoot<out TKey> : IEntity<TKey>
    {
        /// <summary>
        ///     版本
        /// </summary>
        int Version { get; }

        /// <summary>
        ///     是否删除
        /// </summary>
        bool IsDelete { get; }
    }

    /// <summary>
    ///     以Guid作为标识的聚合根
    /// </summary>
    public interface IAggregateRoot : IAggregateRoot<Guid>, IEventSourceable, ISnapshotable
    {
    }
}