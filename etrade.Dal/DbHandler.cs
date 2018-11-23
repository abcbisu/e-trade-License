using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace etrade.dal
{
    public class DbHandler:IDisposable
    {
        SqlConnection cn = new SqlConnection();
        public string ConnectionString
        {
            get { return cn.ConnectionString; }
        }
        public ConnectionState ConnectionState { get { return cn.State; } }
        protected DbHandler()
        {

        }
        public DbHandler(string conString)
        {
            if (cn == null)
                cn = new SqlConnection();
            cn.ConnectionString =ConfigurationManager.ConnectionStrings[ conString].ConnectionString;
        }
        public void ResetConnection(SqlConnection cn)
        {
            this.cn = cn;
        }
        public virtual SqlCommand NewCommand(string commandText, CommandType commandType = CommandType.StoredProcedure)
        {
            var Command = NewCommand();
            Command.CommandText = commandText;
            Command.Connection = cn;
            Command.CommandType = commandType;
            return Command;
        }
        public virtual  SqlCommand NewCommand(string commandText,  SqlTransaction tr, CommandType commandType = CommandType.StoredProcedure)
        {
            var Command = NewCommand();
            Command.CommandText = commandText;
            Command.Connection = cn;
            Command.Transaction = tr;
            Command.CommandType = commandType;
            return Command;
        }
        public SqlCommand NewCommand()
        {
            var command=new SqlCommand();
            command.CommandTimeout = CommandTimeOut();
            return command;
        }
        public void ChangeConnection(SqlConnection cn)
        {
            this.cn = cn;
        }
        public SqlConnection GetConnection()
        {
            return cn ;
        }
        int CommandTimeOut()
        {
            System.Data.SqlClient.SqlConnectionStringBuilder builder = new System.Data.SqlClient.SqlConnectionStringBuilder(ConnectionString);
           return builder.ConnectTimeout;
        }
        public DataSet GetResults(SqlCommand command)
        {
            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter(command);
            da.Fill(ds);
            return ds;
        }
        public DataTable GetResult(SqlCommand command)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(command);
            da.Fill(dt);
            return dt;
        }
        public T GetScalar<T>(SqlCommand cmd)
        {
            if (cn.State != ConnectionState.Open)
                cn.Open();
            var x=cmd.ExecuteScalar();
            return (T)Convert.ChangeType(x, typeof(T));
        }
        public int ExecuteNonQuery(SqlCommand cmd)
        {
            if (cn.State != ConnectionState.Open)
                cn.Open();
            return cmd.ExecuteNonQuery();
        }
        public void CloseConnection()
        {
            cn.Close();
        }
        public void Dispose()
        {
            cn.Close();
            cn.Dispose();
        }
    }
}