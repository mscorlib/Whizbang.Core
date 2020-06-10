namespace Whizbang.Core.Serializer
{
    public interface IObjectSerializer<T>
    {
        T Serialize(object obj, out string typeName);

        object Deserialize(T val, string typeName);
    }
}