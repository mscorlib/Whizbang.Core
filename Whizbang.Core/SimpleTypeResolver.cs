using System;

namespace Whizbang.Core
{
    public class SimpleTypeResolver : ITypeResolver
    {
        public string NameFor(Type type)
        {
            return type.AssemblyQualifiedName;
        }

        public Type Resolve(string typeName)
        {
            return Type.GetType(typeName);
        }
    }
}