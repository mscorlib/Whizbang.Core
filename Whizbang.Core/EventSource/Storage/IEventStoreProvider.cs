using System;
using System.Collections.Generic;
using Whizbang.Core.Domain;

namespace Whizbang.Core.EventSource.Storage
{
    /// <summary>
    ///     领域事件存储器
    /// </summary>
    public interface IEventStoreProvider
    {
        /// <summary>
        ///     保存领域事件
        /// </summary>
        /// <param name="aggregateRoot"></param>
        /// <param name="expectedVersion"></param>
        void Save(IAggregateRoot aggregateRoot, int expectedVersion);

        /// <summary>
        ///     加载领域事件
        /// </summary>
        /// <param name="sourceId">聚合根Id</param>
        /// <returns></returns>
        IEnumerable<IDomainEvent> LoadEvents(Guid sourceId);

        /// <summary>
        ///     加载指定版本号之后的领域事件
        /// </summary>
        /// <param name="sourceId">聚合根Id</param>
        /// <param name="skipVersion">跳过的版本号</param>
        /// <returns></returns>
        IEnumerable<IDomainEvent> LoadEventsFromVersion(Guid sourceId, int skipVersion);
    }
}