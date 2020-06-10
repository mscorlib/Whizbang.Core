using System;

namespace Whizbang.Core.EventSource
{
    /// <summary>
    ///     领域事件基类
    /// </summary>
    public abstract class DomainEvent : Event, IDomainEvent
    {
        protected DomainEvent()
        {
            Id = Guid.NewGuid();
        }

        protected DomainEvent(Guid id)
        {
            Id = id;
        }

        public Guid SourceId { get; set; }

        public int Version { get; set; }
    }
}