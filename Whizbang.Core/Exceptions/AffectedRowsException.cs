namespace Whizbang.Core.Exceptions
{
    /// <summary>
    ///     未注册错误
    /// </summary>
    public class AffectedRowsException : CustomException
    {
        public AffectedRowsException(int affectedRows, int expected, string name = "") :
            base(string.Format("{0} 行 {2} 受影响, 正常行数：{1}", affectedRows, expected, name))
        {
        }
    }
}