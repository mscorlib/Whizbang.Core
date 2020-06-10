using System.Collections.Generic;

namespace Whizbang.Core.Ioc
{
    public interface IContainer
    {
        T Resolve<T>();
        IEnumerable<object> ResolveAll<T>();
    }
}