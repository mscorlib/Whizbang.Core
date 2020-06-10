namespace Whizbang.Core.Thread
{
    /// <summary>
    ///     单例模版
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Singleton<T> where T : class, new()
    {
        private Singleton()
        { }

        private class SingletonCreator
        {
            static SingletonCreator()
            {
            }

            internal static readonly T instance = new T();
        }

        public static T Instance
        {
            get { return SingletonCreator.instance; }
        }
    }
}