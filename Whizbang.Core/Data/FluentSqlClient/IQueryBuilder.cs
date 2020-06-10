using System;
using System.Collections.Generic;
using System.Data;

namespace Whizbang.Core.Data.FluentSqlClient
{
    public interface IQueryBuilder
    {
        #region NoQuery

        IExecutor NonQuery(string query, Action<int> rowsAffected);

        IExecutor NonQuery(string query, IDictionary<string, object> values, Action<int> rowsAffected);

        IExecutor NonQuery(Func<IDbCommand> buildCommand, Action<int> rowsAffected);

        IExecutor NonQuery(string query);

        IExecutor NonQuery(string query, IDictionary<string, object> values);

        IExecutor NonQuery(Func<IDbCommand> buildCommand);

        #endregion NoQuery

        #region DataReader

        IExecutor Reader(string query, Action<IDataReader> rowReader);

        IExecutor Reader(string query, IDictionary<string, object> values, Action<IDataReader> rowReader);

        IExecutor Reader(Func<IDbCommand> buildCommand, Action<IDataReader> rowReader);

        #endregion DataReader

        #region Scalar

        IExecutor Scalar<TResult>(string query, Action<TResult> action);

        IExecutor Scalar<TResult>(string query, IDictionary<string, object> values, Action<TResult> action);

        IExecutor Scalar<TResult>(Func<IDbCommand> buildCommand, Action<TResult> action);

        #endregion Scalar
    }
}