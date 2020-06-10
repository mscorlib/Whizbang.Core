using System;

namespace Whizbang.Core.Ioc
{
    public interface IContainerBuilder
    {
        IDisposableContainer Build();

        IContainerBuilder RegisterSingleton<T>(T instance) where T : class;

        IContainerBuilder RegisterSingleton<T>(T instance, string name) where T : class;

        IContainerBuilder Register<T>() where T : class;

        IContainerBuilder Register<T>(string name) where T : class;

        IContainerBuilder Register<T>(Scope scope) where T : class;

        IContainerBuilder Register<T>(string name, Scope scope) where T : class;

        IContainerBuilder Register<TInterface, TImplementation>()
            where TInterface : class
            where TImplementation : class, TInterface;

        IContainerBuilder Register<TInterface, TImplementation>(string name)
            where TInterface : class
            where TImplementation : class, TInterface;

        IContainerBuilder Register<TInterface, TImplementation>(Scope scope)
            where TInterface : class
            where TImplementation : class, TInterface;

        IContainerBuilder Register<TInterface, TImplementation>(string name, Scope scope)
            where TInterface : class
            where TImplementation : class, TInterface;

        IContainerBuilder Register<T>(Func<IContainer, T> factory) where T : class;

        IContainerBuilder Register<T>(Func<IContainer, T> factory, string name) where T : class;

        IContainerBuilder Register<T>(Func<IContainer, T> factory, Scope scope) where T : class;

        IContainerBuilder Register<T>(Func<IContainer, T> factory, string name, Scope scope) where T : class;

        IContainerBuilder Register(Type a, Type b);

        IContainerBuilder Register(Type a, Type b, string name);

        IContainerBuilder Register(Type a, Type b, Scope scope);
    }

    public enum Scope
    {
        Singleton,
        Transient,
        PerExecutionScope
    }
}