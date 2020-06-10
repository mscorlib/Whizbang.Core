using System;
using Whizbang.Core.Serializer;

namespace Whizbang.Core.EventSource.Storage
{
    /// <summary>
    ///     领域事件持久化对象
    /// </summary>
    public class EventObject : IEntity
    {
        /// <summary>
        ///
        /// </summary>
        public Guid Id { get; private set; }

        /// <summary>
        ///     事件源Id
        /// </summary>
        public Guid SourceId { get; set; }

        /// <summary>
        ///     事件源版本号
        /// </summary>
        public int Version { get; set; }

        /// <summary>
        ///     时间戳
        /// </summary>
        public DateTime Timestamp { get; set; }

        /// <summary>
        ///     事件类型
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        ///     事件序列化数据
        /// </summary>
        public string Data { get; set; }

        /// <summary>
        ///     从领域事件创建持久化对象
        /// </summary>
        /// <param name="event">领域事件</param>
        /// <returns></returns>
        public static EventObject FromEvent(IDomainEvent @event)
        {
            var obj = new EventObject
            {
                Id = @event.Id,
                SourceId = @event.SourceId,
                Timestamp = DateTime.UtcNow,
                Version = @event.Version,
            };

            var serializer = App.Container.Resolve<IObjectSerializer<string>>();

            string typeName;
            obj.Data = serializer.Serialize(@event, out typeName);
            obj.Type = typeName;

            return obj;
        }

        /// <summary>
        ///     从持久化对象创建领域事件
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static IDomainEvent ToEvent(EventObject obj)
        {
            var serializer = App.Container.Resolve<IObjectSerializer<string>>();

            return (IDomainEvent)serializer.Deserialize(obj.Data, obj.Type);
        }
    }
}