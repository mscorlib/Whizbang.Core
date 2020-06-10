using System;
using System.Collections.Generic;
using System.Data;

namespace Whizbang.Core.Data.FluentSqlClient
{
    public abstract class Executor : IExecutor
    {
        private readonly IList<Action<IDbConnection, IDbTransaction>> _actions;
        private readonly Action<IDbCommand, string, object> _addParam;
        private readonly Func<IDbCommand> _getCmd;
        private readonly Func<IDbConnection> _getConn;

        protected Executor(
            Func<IDbConnection> getConn,
            Func<IDbCommand> getCmd,
            Action<IDbCommand, string, object> addParam)
        {
            _getConn = getConn;
            _getCmd = getCmd;
            _addParam = addParam;

            _actions = new List<Action<IDbConnection, IDbTransaction>>();
        }

        #region NonQuery

        public IExecutor NonQuery(string query, Action<int> rowsAffected)
        {
            return NonQuery(BuildCommand(query), rowsAffected);
        }

        public IExecutor NonQuery(string query, IDictionary<string, object> values, Action<int> rowsAffected)
        {
            return NonQuery(BuildCommand(query, values), rowsAffected);
        }

        public IExecutor NonQuery(Func<IDbCommand> buildCommand, Action<int> rowsAffected)
        {
            _actions.Add((conn, trans) => ExecuteNonQuery(conn, trans, buildCommand, rowsAffected));
            return this;
        }

        public IExecutor NonQuery(string query)
        {
            return NonQuery(query, rowsAffected => { });
        }

        public IExecutor NonQuery(string query, IDictionary<string, object> values)
        {
            return NonQuery(query, values, rowsAffected => { });
        }

        public IExecutor NonQuery(Func<IDbCommand> buildCommand)
        {
            return NonQuery(buildCommand, rowsAffected => { });
        }

        #endregion NonQuery

        #region DataReader

        public IExecutor Reader(string query, Action<IDataReader> rowReader)
        {
            return Reader(BuildCommand(query), rowReader);
        }

        public IExecutor Reader(string query, IDictionary<string, object> values, Action<IDataReader> rowReader)
        {
            return Reader(BuildCommand(query, values), rowReader);
        }

        public IExecutor Reader(Func<IDbCommand> buildCommand, Action<IDataReader> rowReader)
        {
            _actions.Add((conn, trans) => ExecuteReader(conn, trans, buildCommand, rowReader));
            return this;
        }

        #endregion DataReader

        #region Saclar

        public IExecutor Scalar<TResult>(string query, Action<TResult> action)
        {
            return Scalar(BuildCommand(query), action);
        }

        public IExecutor Scalar<TResult>(string query, IDictionary<string, object> values, Action<TResult> action)
        {
            return Scalar(BuildCommand(query, values), action);
        }

        public IExecutor Scalar<TResult>(Func<IDbCommand> buildCommand, Action<TResult> action)
        {
            _actions.Add((conn, trans) => ExecuteScalar(conn, trans, buildCommand, action));
            return this;
        }

        #endregion Saclar

        public void Execute()
        {
            using (IDbConnection conn = _getConn())
            {
                conn.Open();

                using (IDbTransaction trans = conn.BeginTransaction())
                {
                    foreach (var action in _actions)
                        action(conn, trans);
                    trans.Commit();
                }

                conn.Close();
            }
        }

        private Func<IDbCommand> BuildCommand(string query)
        {
            return BuildCommand(query, new Dictionary<string, object>());
        }

        private Func<IDbCommand> BuildCommand(string query, IDictionary<string, object> cmdParams)
        {
            //Dictionary<string, object> cmdParams = values.ToDictionary(i => i.Key, i => i.Value);

            Func<IDbCommand> builder = () =>
            {
                IDbCommand cmd = _getCmd();
                cmd.CommandText = query;
                foreach (var paramData in cmdParams)
                    _addParam(cmd, paramData.Key, paramData.Value);
                return cmd;
            };

            return builder;
        }

        private void ExecuteScalar<TResult>(
            IDbConnection connection,
            IDbTransaction transaction,
            Func<IDbCommand> getCommand,
            Action<TResult> action)
        {
            Execute(connection, transaction, getCommand,
                cmd =>
                {
                    var result = (TResult)cmd.ExecuteScalar();
                    action(result);
                });
        }

        private void ExecuteReader(
            IDbConnection connection,
            IDbTransaction transaction,
            Func<IDbCommand> getCommand,
            Action<IDataReader> forEachRow)
        {
            Execute(connection, transaction, getCommand,
                cmd =>
                {
                    using (IDataReader rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                            forEachRow(rdr);
                        rdr.Close();
                    }
                });
        }

        private void ExecuteNonQuery(
            IDbConnection connection,
            IDbTransaction transaction,
            Func<IDbCommand> getCommand,
            Action<int> rowsAffectedAction
            )
        {
            Execute(connection, transaction, getCommand,
                cmd =>
                {
                    int rowsAffected = cmd.ExecuteNonQuery();
                    rowsAffectedAction(rowsAffected);
                });
        }

        private void Execute(
            IDbConnection connection,
            IDbTransaction transaction,
            Func<IDbCommand> getCommand,
            Action<IDbCommand> action)
        {
            using (IDbCommand cmd = getCommand())
            {
                cmd.Connection = connection;
                cmd.Transaction = transaction;
                action(cmd);
            }
        }
    }
}