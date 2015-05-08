using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data.SQLite;
using System.Data;
using System.Configuration;
using System.Threading;
using System.Text.RegularExpressions;

namespace Spider.DataAccess
{
    public class SQLiteHelper
    {

        private static string connString;
        private static string ConnectionString
        {
            get
            {
                if (!string.IsNullOrEmpty(connString))
                {
                    return connString;
                }
                if (ConfigurationManager.ConnectionStrings["SpiderSQLite"] != null)
                {
                    connString = ConfigurationManager.ConnectionStrings["SpiderSQLite"].ConnectionString;
                    return connString;
                }
                string appPath = System.Environment.CurrentDirectory;
                connString = string.Format("Data Source={0}\\Stock.db", appPath);
                return connString;
            }
        }

        private SQLiteConnection conn;
        private Dictionary<int, SQLiteTransaction> localTransCollection;

        int transCount = 0;
        private object locker = new object();

        private SQLiteHelper()
        { }
        public static SQLiteHelper Instance = new SQLiteHelper();

        private void OpenConnection()
        {
            if (this.conn == null)
            {
                this.conn = new SQLiteConnection(ConnectionString);
            }
            if (this.conn.State == ConnectionState.Closed)
            {
                this.conn.Open();
            }
        }
        private Dictionary<int, SQLiteTransaction> LocalTransCollection
        {
            get
            {
                lock (locker)
                {
                    if (this.localTransCollection == null)
                    {
                        this.localTransCollection = new Dictionary<int, SQLiteTransaction>();
                    }
                    return this.localTransCollection;
                }
            }

        }

        private SQLiteCommand CreateCommand(string sql, params object[] parameters)
        {
            SQLiteCommand cmd = null;
            int managedThreadId = Thread.CurrentThread.ManagedThreadId;
            if (this.LocalTransCollection.ContainsKey(managedThreadId) && this.LocalTransCollection[managedThreadId] != null)
            {
                cmd = new SQLiteCommand(sql, this.conn, this.LocalTransCollection[managedThreadId]);
            }
            else
            {
                cmd = new SQLiteCommand(sql, this.conn);
            }
            List<SQLiteParameter> cmdParams = this.DeriveParameters(sql, parameters);
            if (cmdParams != null)
            {
                foreach (var param in cmdParams)
                {
                    cmd.Parameters.Add(param);
                }
            }
            return cmd;
        }
        private List<SQLiteParameter> DeriveParameters(string cmdText, params object[] parameters)
        {
            if (parameters == null || parameters.Count() == 0)
                return null;

            List<SQLiteParameter> paramList = new List<SQLiteParameter>();

            string input = cmdText.Substring(cmdText.IndexOf("@")).Replace(",", " ,").Replace(")", " )");
            string pattern = @"(@)\S*(.*?)\b";
            MatchCollection matchs = new Regex(pattern, RegexOptions.IgnoreCase).Matches(input);
            List<string> list = new List<string>();
            foreach (Match match in matchs)
            {
                if (!list.Contains(match.Value))
                {
                    list.Add(match.Value);
                }
            }

            string[] paramNames = list.ToArray();
            int index = 0;
            Type type = null;
            foreach (var obj in parameters)
            {
                SQLiteParameter param = new SQLiteParameter();
                param.ParameterName = paramNames[index];
                paramList.Add(param);
                index++;
                if (obj == null)
                {
                    param.DbType = DbType.Object;
                    param.Value = DBNull.Value;
                    continue;
                }

                type = obj.GetType();
                switch (type.ToString())
                {
                    case "System.String":
                        param.DbType = DbType.String;
                        param.Value = (string)obj;
                        break;
                    case "System.Byte[]":
                        param.DbType = DbType.Binary;
                        param.Value = (byte[])obj;
                        break;
                    case "System.Int64":
                        param.DbType = DbType.Int64;
                        param.Value = (long)obj;
                        break;
                    case "System.Int32":
                        param.DbType = DbType.Int32;
                        param.Value = (int)obj;
                        break;
                    case "System.Boolean":
                        param.DbType = DbType.Boolean;
                        param.Value = (bool)obj;
                        break;
                    case "System.DateTime":
                        param.DbType = DbType.DateTime;
                        param.Value = Convert.ToDateTime(obj);
                        break;
                    case "System.Double":
                    case "System.Single":
                        param.DbType = DbType.Double;
                        param.Value = Convert.ToDouble(obj);
                        break;
                    case "System.Decimal":
                        param.DbType = DbType.Decimal;
                        param.Value = Convert.ToDecimal(obj);
                        break;
                    case "System.Guid":
                        param.DbType = DbType.Guid;
                        param.Value = (Guid)obj;
                        break;
                    case "System.Object":
                        param.DbType = DbType.Object;
                        param.Value = obj;
                        break;
                    default:
                        throw new SystemException("Value is of unknown data type");
                }
            }
            return paramList;
        }

        public void AppendTransaction()
        {
            lock (locker)
            {
                this.OpenConnection();
                transCount++;
                int threadId = Thread.CurrentThread.ManagedThreadId;
                if (!this.LocalTransCollection.ContainsKey(threadId))
                {
                    this.LocalTransCollection.Add(threadId, this.conn.BeginTransaction());
                }
            }
        }
        public void CommitTransaction()
        {
            lock (locker)
            {
                int threadId = Thread.CurrentThread.ManagedThreadId;
                if (this.LocalTransCollection.ContainsKey(threadId))
                {
                    this.LocalTransCollection[threadId].Commit();
                    transCount--;
                    this.LocalTransCollection.Remove(threadId);
                    if (transCount == 0)
                    {
                        conn.Close();
                    }
                }
            }
        }
        public void RollbackTransaction()
        {
            lock (locker)
            {
                int threadId = Thread.CurrentThread.ManagedThreadId;
                if (this.LocalTransCollection.ContainsKey(threadId))
                {
                    this.LocalTransCollection[threadId].Rollback();
                    transCount--;
                    this.LocalTransCollection.Remove(threadId);
                    if (transCount == 0)
                    {
                        this.conn.Close();
                    }
                }
            }
        }

        public int ExecuteNonQuery(string sql, params object[] parameters)
        {
            this.OpenConnection();
            return this.CreateCommand(sql, parameters).ExecuteNonQuery();
        }

        public SQLiteDataReader ExecuteReader(string sql, params object[] parameters)
        {
            this.OpenConnection();
            return this.CreateCommand(sql, parameters).ExecuteReader();
        }

        public object ExecuteScalar(string sql, params object[] parameters)
        {
            this.OpenConnection();
            return this.CreateCommand(sql, parameters).ExecuteScalar();
        }

    }
}
