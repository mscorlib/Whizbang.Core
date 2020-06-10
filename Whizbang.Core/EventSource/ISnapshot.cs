using System;

namespace Whizbang.Core.EventSource
{
    /// <summary>
    ///     快照接口
    /// </summary>
    public interface ISnapshot
    {
        /// <summary>
        ///     时间戳
        /// </summary>
        DateTime Timestamp { get; }

        /// <summary>
        ///     聚合根Id
        /// </summary>
        Guid SourceId { get; }

        /// <summary>
        ///     版本
        /// </summary>
        int Version { get; }
    }
}