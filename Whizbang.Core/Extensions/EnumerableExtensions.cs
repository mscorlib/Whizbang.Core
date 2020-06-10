using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace Whizbang.Core.Extensions
{
    public static class EnumerableExtensions
    {
        /// <summary>
        ///     遍历集合
        /// </summary>
        /// <typeparam name="T">集合元素类型</typeparam>
        /// <param name="array">集合实例</param>
        /// <param name="action">执行的方法</param>
        public static void Each<T>(this IEnumerable<T> array, Action<T> action)
        {
            foreach (var item in array)
            {
                action.Invoke(item);
            }
        }

        /// <summary>
        ///     尝试弹出队列首个元素
        /// </summary>
        /// <typeparam name="T">队列元素类型</typeparam>
        /// <param name="queue">队列</param>
        /// <param name="action">执行的方法</param>
        public static void TryOut<T>(this ConcurrentQueue<T> queue, Action<T> action)
        {
            while (0 < queue.Count)
            {
                T t;
                if (queue.TryDequeue(out t))
                {
                    action.Invoke(t);
                }
            }
        }
    }
}