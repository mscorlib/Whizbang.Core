namespace Whizbang.Core.Exceptions
{
    /// <summary>
    ///     未注册错误
    /// </summary>
    public class UnregisteredException : CustomException
    {
        /// <summary>
        ///     实例化一个未注册错误
        /// </summary>
        /// <param name="message">错误信息</param>
        public UnregisteredException(string message) : base(message)
        {
        }
    }
}