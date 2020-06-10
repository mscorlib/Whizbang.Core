using System;
using Whizbang.Core.Serializer;

namespace Whizbang.Core.EventSource.Storage
{
    /// <summary>
    ///     领域事件持久化对象
    /// </summary>
    public class SnapshotObject
    {
        /// <summary>
        ///     事件源Id
        /// </summary>
        public Guid SourceId { get; set; }

        /// <summary>
        ///     版本号
        /// </summary>
        public int Version { get; set; }

        /// <summary>
        ///     时间戳
        /// </summary>
        public DateTime Timestamp { get; set; }

        /// <summary>
        ///     快照类型
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        ///     快照序列化数据
        /// </summary>
        public string Data { get; set; }

        /// <summary>
        ///     从快照创建快照持久化对象
        /// </summary>
        /// <param name="snapshot">领域事件</param>
        /// <returns></returns>
        public static SnapshotObject FromSnapshot(ISnapshot snapshot)
        {
            var obj = new SnapshotObject
            {
                SourceId = snapshot.SourceId,
                Timestamp = DateTime.UtcNow,
                Version = snapshot.Version,
            };

            var serializer = App.Container.Resolve<IObjectSerializer<string>>();

            string typeName;
            obj.Data = serializer.Serialize(snapshot, out typeName);
            obj.Type = typeName;

            return obj;
        }

        /// <summary>
        ///     从持久化数据加载快照
        /// </summary>
        /// <param name="obj">持久化数据</param>
        /// <returns></returns>
        public static ISnapshot ToSnapshot(SnapshotObject obj)
        {
            var serializer = App.Container.Resolve<IObjectSerializer<string>>();

            return (ISnapshot)serializer.Deserialize(obj.Data, obj.Type);
        }
    }
}