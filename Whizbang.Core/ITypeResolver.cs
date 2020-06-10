using System;

namespace Whizbang.Core
{
    public interface ITypeResolver
    {
        Type Resolve(string typeName);

        string NameFor(Type type);
    }
}