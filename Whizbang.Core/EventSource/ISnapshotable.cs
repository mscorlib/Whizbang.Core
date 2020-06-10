using System.Collections.Generic;

namespace Whizbang.Core.EventSource
{
    /// <summary>
    ///     提供快照接口
    /// </summary>
    public interface ISnapshotable
    {
        /// <summary>
        ///     是否已创建快照
        /// </summary>
        bool Snapshotted { get; }

        /// <summary>
        ///     从指定快照版本创建对象
        /// </summary>
        /// <param name="snapshot"></param>
        void BuildFromSnapshot(ISnapshot snapshot);

        /// <summary>
        ///     未保存事件生产的快照集合
        /// </summary>
        /// <returns></returns>
        IEnumerable<ISnapshot> Snapshots { get; }
    }
}