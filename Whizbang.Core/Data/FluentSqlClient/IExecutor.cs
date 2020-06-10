namespace Whizbang.Core.Data.FluentSqlClient
{
    public interface IExecutor : IQueryBuilder
    {
        void Execute();
    }
}