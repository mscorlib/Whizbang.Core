using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Whizbang.Core.Domain;

namespace Whizbang.Core.EventSource.Storage
{
    public abstract class EventStorageBase : IEventStorage
    {
        private readonly IEventBus _bus;

        protected EventStorageBase()
        {
            _bus = App.Container.Resolve<IEventBus>();
        }

        public void Save(IAggregateRoot aggregateRoot, int expectedVersion)
        {
            var domainEvents = aggregateRoot.UncommittedEvents as IDomainEvent[] ?? aggregateRoot.UncommittedEvents.ToArray();

            if (aggregateRoot.Snapshotted)
            {
                foreach (var snapshot in aggregateRoot.Snapshots)
                {
                    PersistSnapshot(SnapshotObject.FromSnapshot(snapshot));
                }
            }

            SaveEvents(aggregateRoot.Id, domainEvents, expectedVersion);

            Task.Factory.StartNew(() => PublishEvents(domainEvents));
        }

        public IEnumerable<IDomainEvent> LoadEvents(Guid sourceId)
        {
            var objs = LoadEventObjects(sourceId, Constants.InitialAggregateRootVersion);

            return objs.Select(EventObject.ToEvent);
        }

        public IEnumerable<IDomainEvent> LoadEventsFromVersion(Guid sourceId, int skipVersion)
        {
            var objs = LoadEventObjects(sourceId, skipVersion);

            return objs.Select(EventObject.ToEvent);
        }

        public ISnapshot GetSnapshot(Guid sourceId)
        {
            var snapshotObj = LoadSnapshotObject(sourceId);

            return null == snapshotObj ? null : SnapshotObject.ToSnapshot(snapshotObj);
        }

        /// <summary>
        ///     加载事件持久化对象
        /// </summary>
        /// <param name="sourceId"></param>
        /// <param name="skipVersion"></param>
        /// <returns></returns>
        protected abstract IEnumerable<EventObject> LoadEventObjects(Guid sourceId, int skipVersion);

        /// <summary>
        ///     持久化事件
        /// </summary>
        /// <param name="eventObjects"></param>
        /// <param name="aggregateId"></param>
        /// <param name="expectedVersion"></param>
        protected abstract void PersistEvent(IEnumerable<EventObject> eventObjects, Guid aggregateId, int expectedVersion);

        /// <summary>
        ///     加载快照持久化对象
        /// </summary>
        /// <param name="sourceId"></param>
        /// <returns></returns>
        protected abstract SnapshotObject LoadSnapshotObject(Guid sourceId);

        /// <summary>
        ///     持久化快照
        /// </summary>
        /// <param name="snapshotObject"></param>
        protected abstract void PersistSnapshot(SnapshotObject snapshotObject);

        private void SaveEvents(Guid eventSourceId, IEnumerable<IDomainEvent> events, int expectedVersion)
        {
            var domainEvents = events as IDomainEvent[] ?? events.ToArray();

            if (!domainEvents.Any())
                return;

            var eventObjs = domainEvents.Select(EventObject.FromEvent).ToList();

            PersistEvent(eventObjs, eventSourceId, expectedVersion);
        }

        private void PublishEvents(IEnumerable<IDomainEvent> evnts)
        {
            _bus.Publish(evnts);
        }
    }
}