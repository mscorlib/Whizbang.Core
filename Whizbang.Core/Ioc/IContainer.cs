using System.Collections.Generic;

namespace Whizbang.Core.Ioc
{
    public interface IContainer
    {
        bool IsRegistered<T>();

        bool IsRegistered<T>(string name);

        T Resolve<T>();

        T Resolve<T>(string name);

        IEnumerable<object> ResolveAll<T>();
    }
}