using System;

namespace Whizbang.Core
{
    public static class Converter
    {
        public static Action<object> Convert<T>(Action<T> myActionT)
        {
            return myActionT == null ? null : new Action<object>(o => myActionT((T)o));
        }

        public static dynamic ChangeTo(dynamic source, Type dest)
        {
            return System.Convert.ChangeType(source, dest);
        }
    }
}