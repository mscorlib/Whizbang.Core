using System;

namespace Whizbang.Core.EventSource
{
    /// <summary>
    ///     事件基类
    /// </summary>
    public abstract class Event : IEvent
    {
        protected Event()
        {
            Timestamp = DateTime.Now;
        }

        public Guid Id { get; protected set; }
        public DateTime Timestamp { get; private set; }
    }
}