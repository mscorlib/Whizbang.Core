using System;
using Whizbang.Core.Domain;

namespace Whizbang.Core.EventSource.Storage
{
    public interface IDomainRepository<TAggregateRoot> where TAggregateRoot : IAggregateRoot
    {
        /// <summary>
        ///     保存聚合根
        /// </summary>
        /// <param name="aggregate">聚合根</param>
        /// <param name="expectedVersion">当前执行的聚合根版本号</param>
        void Save(TAggregateRoot aggregate, int expectedVersion);

        /// <summary>
        ///     根据Id获取聚合根
        /// </summary>
        /// <param name="id">聚合根Id</param>
        /// <returns></returns>
        TAggregateRoot GetById(Guid id);
    }
}