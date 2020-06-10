namespace Whizbang.Core.EventSource.Storage
{
    public interface IEventStorage : IEventStoreProvider, ISnapshotStoreProvider
    {
    }
}