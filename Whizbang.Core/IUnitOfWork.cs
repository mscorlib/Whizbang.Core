namespace Whizbang.Core
{
    /// <summary>
    ///     工作单元
    /// </summary>
    public interface IUnitOfWork
    {
        /// <summary>
        ///     提交
        /// </summary>
        void Commit();

        /// <summary>
        ///     回滚
        /// </summary>
        void Rollback();
    }
}