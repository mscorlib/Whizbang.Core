using System;
using System.Collections.Generic;
using Whizbang.Core.EventSource;

namespace Whizbang.Core.Domain
{
    /// <summary>
    ///     聚合根基类
    /// </summary>
    public abstract class AggregateRoot : IAggregateRoot
    {
        private readonly List<IDomainEvent> _changes = new List<IDomainEvent>();
        private readonly List<ISnapshot> _snapshots = new List<ISnapshot>();

        //protected AggregateRoot(Guid id, int version)
        //{
        //    Id = id;
        //    Version = version;
        //}

        public Guid Id { get; protected set; }

        public int Version { get; protected set; }

        public bool IsDelete { get; protected set; }

        public IEnumerable<IDomainEvent> UncommittedEvents
        {
            get { return _changes; }
        }

        public void BuildFromEvents(IEnumerable<IDomainEvent> historicalEvents)
        {
            foreach (var @event in historicalEvents)
            {
                ApplyChange(@event, false);
            }
        }

        public bool Snapshotted { get; private set; }

        public IEnumerable<ISnapshot> Snapshots { get { return _snapshots; } }

        public abstract void BuildFromSnapshot(ISnapshot snapshot);

        public abstract void Delete(Guid id);

        public void ApplyChange(IDomainEvent @event)
        {
            ApplyChange(@event, true);
        }

        public void ApplyChange(IDomainEvent @event, bool isNew)
        {
            this.AsDynamic().Apply(@event);

            if (isNew)
            {
                Version++;

                @event.Version = Version;

                if (ShouldCreateSnapshot(@event))
                {
                    Snapshotted = true;

                    var snapshot = CreateSnapshot();

                    snapshot.SourceId = Id;
                    snapshot.Version = Version;

                    _snapshots.Add(snapshot);
                }

                _changes.Add(@event);
            }
            else
            {
                Version = @event.Version;
            }
        }

        protected abstract Snapshot CreateSnapshot();

        private bool ShouldCreateSnapshot(IDomainEvent @event)
        {
            if (Constants.InitialAggregateRootVersion >= @event.Version)
                return false;

            return (@event.Version - Constants.InitialAggregateRootVersion) % Constants.DefaultSnapshotIntervalInEvents == 0;
        }

        private void Apply(DeleteDomianEvent @event)
        {
            IsDelete = true;
        }
    }
}