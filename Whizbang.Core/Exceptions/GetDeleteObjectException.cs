namespace Whizbang.Core.Exceptions
{
    /// <summary>
    ///     获取一个已删除对象错误
    /// </summary>
    public class GetDeleteObjectException : CustomException
    {
        public GetDeleteObjectException(string message)
            : base(message)
        {
        }
    }
}