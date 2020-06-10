namespace Whizbang.Core.Exceptions
{
    /// <summary>
    ///     并发错误
    /// </summary>
    public class ConcurrencyException : CustomException
    {
        /// <summary>
        ///     实例化一个并发错误
        /// </summary>
        /// <param name="message">错误信息</param>
        public ConcurrencyException(string message)
            : base(message)
        {
        }
    }
}