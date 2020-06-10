using System;

namespace Whizbang.Core.Ioc
{
    public interface IContainerBuilder
    {
        IContainer Build();

        IContainerBuilder RegisterSingleton<T>(T instance) where T : class;

        IContainerBuilder Register<T>() where T : class;

        IContainerBuilder Register<T>(Scope scope) where T : class;

        IContainerBuilder Register<TInterface, TImplementation>()
            where TInterface : class
            where TImplementation : class, TInterface;

        IContainerBuilder Register<TInterface, TImplementation>(Scope scope)
            where TInterface : class
            where TImplementation : class, TInterface;

        IContainerBuilder Register(Type a, Type b);

        IContainerBuilder Register(Type a, Type b, Scope scope);
    }

    public enum Scope
    {
        Singleton,
        Transient,
        PerExecutionScope
    }
}