using System;

namespace Whizbang.Core
{
    /// <summary>
    ///     基础实体接口
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    public interface IEntity<out TKey>
    {
        TKey Id { get; }
    }

    /// <summary>
    ///     以Guid作为标识的实体接口
    /// </summary>
    public interface IEntity : IEntity<Guid>
    {
    }
}