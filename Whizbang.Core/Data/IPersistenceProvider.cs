using System;
using System.Collections.Generic;

namespace Whizbang.Core.Data
{
    public interface IPersistenceProvider
    {
        void Save<TEntity>(TEntity entity) where TEntity : IEntity;

        IEnumerable<TEntity> GetById<TEntity>(Guid id) where TEntity : IEntity;
    }
}