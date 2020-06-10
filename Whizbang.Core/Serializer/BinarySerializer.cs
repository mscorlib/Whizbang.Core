using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Whizbang.Core.Serializer
{
    public class BinarySerializer : IObjectSerializer<byte[]>
    {
        private readonly ITypeResolver _typeResolver;

        public BinarySerializer(ITypeResolver typeResolver)
        {
            _typeResolver = typeResolver;
        }

        private readonly BinaryFormatter _binaryFormatter = new BinaryFormatter();

        public byte[] Serialize(object obj, out string typeName)
        {
            typeName = _typeResolver.NameFor(obj.GetType());

            byte[] ret;
            using (var ms = new MemoryStream())
            {
                _binaryFormatter.Serialize(ms, obj);
                ret = ms.ToArray();
                ms.Close();
            }
            return ret;
        }

        public object Deserialize(byte[] val, string typeName)
        {
            using (var ms = new MemoryStream(val))
            {
                var ret = _binaryFormatter.Deserialize(ms);
                ms.Close();
                return ret;
            }
        }
    }
}