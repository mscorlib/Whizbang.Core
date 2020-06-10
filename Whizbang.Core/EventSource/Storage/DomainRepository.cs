using System;
using System.Collections.Generic;
using System.Linq;
using Whizbang.Core.Domain;
using Whizbang.Core.Exceptions;

namespace Whizbang.Core.EventSource.Storage
{
    public class DomainRepository<TAggregateRoot> : IDomainRepository<TAggregateRoot>
        where TAggregateRoot : AggregateRoot, new()
    {
        /// <summary>
        ///     保存聚合根
        /// </summary>
        /// <param name="aggregate">聚合根实例</param>
        /// <param name="expectedVersion">预期版本</param>
        public void Save(TAggregateRoot aggregate, int expectedVersion)
        {
            if (!aggregate.UncommittedEvents.Any()) return;

            //var locker = Singleton<NamedLock>.Instance;

            //锁住聚合根
            //var lockName = aggregate.Id.ToString("N");

            var storage = App.Container.Resolve<IEventStorage>();
            storage.Save(aggregate, expectedVersion);

            //locker.Lock(lockName, () => storage.Save(aggregate, expectedVersion));
        }

        /// <summary>
        ///     根据Id获取聚合根
        /// </summary>
        /// <param name="id">聚合根Id</param>
        /// <returns></returns>
        public TAggregateRoot GetById(Guid id)
        {
            var storage = App.Container.Resolve<IEventStorage>();

            var snapshot = storage.GetSnapshot(id);

            IEnumerable<IDomainEvent> events = null == snapshot
                ? storage.LoadEvents(id)
                : storage.LoadEventsFromVersion(id, snapshot.Version);

            var aggegateRoot = new TAggregateRoot();

            var ar = aggegateRoot.AsDynamic();
            ar.Id = id;

            if (null != snapshot)
            {
                aggegateRoot.BuildFromSnapshot(snapshot);
                ar.Version = snapshot.Version;
            }

            aggegateRoot.BuildFromEvents(events.OrderBy(x => x.Version));

            if (aggegateRoot.IsDelete)
                throw new GetDeleteObjectException("聚合根已删除，不能修改！");

            return aggegateRoot;
        }
    }
}