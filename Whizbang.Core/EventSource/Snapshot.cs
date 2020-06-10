using System;

namespace Whizbang.Core.EventSource
{
    /// <summary>
    ///     快照对象基类
    /// </summary>
    public abstract class Snapshot : ISnapshot
    {
        protected Snapshot()
        {
            Timestamp = DateTime.UtcNow;
        }

        public DateTime Timestamp { get; private set; }

        public Guid SourceId { get; set; }

        public int Version { get; set; }
    }
}