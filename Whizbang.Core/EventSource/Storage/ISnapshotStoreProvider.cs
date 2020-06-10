using System;

namespace Whizbang.Core.EventSource.Storage
{
    public interface ISnapshotStoreProvider
    {
        ISnapshot GetSnapshot(Guid sourceId);
    }
}