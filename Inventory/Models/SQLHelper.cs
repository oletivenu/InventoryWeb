using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Inventory.Models
{
    public class SQLHelper
    {
        protected SqlConnection connection;
        private readonly string connectionString;

        /// <summary>
        /// A parameterized constructor, it allows us to take a connection
        /// string as a constructor argument, automatically instantiating
        /// a new connection.
        /// </summary>
        /// <param name="newConnectionString">Connection String to the associated database</param>
        public SQLHelper(string newConnectionString)
        {
            connectionString = newConnectionString;
            connection = new SqlConnection(connectionString);
        }

        public SQLHelper()
        {
            connectionString = ConfigurationManager.ConnectionStrings["MDFConnection"].ConnectionString;
            connection = new SqlConnection(connectionString);
        }

        /// <summary>
        /// Protected property that exposes the connection string
        /// to inheriting classes. Read-Only.
        /// </summary>
        protected string ConnectionString
        {
            get
            {
                return connectionString;
            }
        }

        /// <summary>
        /// Private routine allowed only by this base class, it automates the task
        /// of building a SqlCommand object designed to obtain a return value from
        /// the stored procedure.
        /// </summary>
        /// <param name="storedProcName">Name of the stored procedure in the DB, eg. sp_DoTask</param>
        /// <param name="parameters">Array of IDataParameter objects containing parameters to the stored proc</param>
        /// <returns>Newly instantiated SqlCommand instance</returns>
        private SqlCommand BuildIntCommand(string storedProcName, IDataParameter[] parameters)
        {
            SqlCommand command = BuildQueryCommand(storedProcName, parameters);

            command.Parameters.Add(new SqlParameter("ReturnValue",
                SqlDbType.Int,
                4,
                /* Size */ ParameterDirection.ReturnValue,
                false,
                /* is nullable */ 0,
                /* byte precision */ 0,
                /* byte scale */ string.Empty,
                DataRowVersion.Default,
                null));

            return command;
        }

        /// <summary>
        /// Builds a SqlCommand designed to return a SqlDataReader, and not
        /// an actual integer value.
        /// </summary>
        /// <param name="storedProcName">Name of the stored procedure</param>
        /// <param name="parameters">Array of IDataParameter objects</param>
        /// <returns></returns>
        private SqlCommand BuildQueryCommand(string storedProcName, IDataParameter[] parameters)
        {
            SqlCommand command = new SqlCommand(storedProcName, connection);
            command.CommandType = CommandType.StoredProcedure;

            foreach (SqlParameter parameter in parameters)
            {
                command.Parameters.Add(parameter);
            }

            return command;
        }

        /// <summary>
        /// Runs a stored procedure, can only be called by those classes deriving
        /// from this base. It returns an integer indicating the return value of the
        /// stored procedure, and also returns the value of the RowsAffected aspect
        /// of the stored procedure that is returned by the ExecuteNonQuery method.
        /// </summary>
        /// <param name="storedProcName">Name of the stored procedure</param>
        /// <param name="parameters">Array of IDataParameter objects</param>
        /// <param name="rowsAffected">Number of rows affected by the stored procedure.</param>
        /// <returns>An integer indicating return value of the stored procedure</returns>
        public int RunProcedure(string storedProcName, IDataParameter[] parameters, out int rowsAffected)
        {
            int result;

            connection.Open();
            SqlCommand command = BuildIntCommand(storedProcName, parameters);
            rowsAffected = command.ExecuteNonQuery();
            result = (int)command.Parameters["ReturnValue"].Value;
            connection.Close();
            return result;
        }

        public int RunProcedure(string storedProcName, IDataParameter[] parameters, bool forceOutParameter)
        {
            //int result;
            connection.Open();
            SqlCommand command = BuildIntCommand(storedProcName, parameters);
            //command.Parameters["ReturnValue"].Direction = ParameterDirection.Output;

            object result = command.ExecuteScalar();
            connection.Close();
            if (result != null)
                return Convert.ToInt32(result);
            else
                return 0;
            //(int)command.Parameters["ReturnValue"].Value;
            //return result;
        }

        public string ExecuteScalar(string storedProcName, IDataParameter[] parameters)
        {
            //int result;
            connection.Open();
            SqlCommand command = BuildIntCommand(storedProcName, parameters);
            //command.Parameters["ReturnValue"].Direction = ParameterDirection.Output;

            object result = command.ExecuteScalar();
            connection.Close();
            if (result != null)
                return Convert.ToString(result);
            else
                return null;
        }

        /// <summary>
        /// Will run a stored procedure, can only be called by those classes deriving
        /// from this base. It returns a SqlDataReader containing the result of the stored
        /// procedure.
        /// </summary>
        /// <param name="storedProcName">Name of the stored procedure</param>
        /// <param name="parameters">Array of parameters to be passed to the procedure</param>
        /// <returns>A newly instantiated SqlDataReader object</returns>
        public SqlDataReader RunProcedure(string storedProcName, IDataParameter[] parameters)
        {
            SqlDataReader returnReader;

            connection.Open();
            SqlCommand command = BuildQueryCommand(storedProcName, parameters);
            command.CommandType = CommandType.StoredProcedure;

            returnReader = command.ExecuteReader();
            //Connection.Close();
            return returnReader;
        }

        /// <summary>
        /// Creates a DataSet by running the stored procedure and placing the results
        /// of the query/proc into the given tablename.
        /// </summary>
        /// <param name="storedProcName"></param>
        /// <param name="parameters"></param>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public DataTable RunProcedureAndReturnDT(string storedProcName, IDataParameter[] parameters)
        {
            DataSet dataSet = new DataSet();
            connection.Open();
            SqlDataAdapter sqlDA = new SqlDataAdapter();
            sqlDA.SelectCommand = BuildQueryCommand(storedProcName, parameters);
            sqlDA.Fill(dataSet, "DataTable");
            connection.Close();

            if ((dataSet == null) || (dataSet.Tables.Count == 0))
            {
                return null;
            }
            else
                return dataSet.Tables[0];
        }

        public DataSet RunProcedure(string storedProcName, IDataParameter[] parameters, string tableName)
        {
            DataSet dataSet = new DataSet();
            connection.Open();
            SqlDataAdapter sqlDA = new SqlDataAdapter();
            sqlDA.SelectCommand = BuildQueryCommand(storedProcName, parameters);
            sqlDA.Fill(dataSet, tableName);
            connection.Close();

            return dataSet;
        }

        /// <summary>
        /// Takes an -existing- dataset and fills the given table name with the results
        /// of the stored procedure.
        /// </summary>
        /// <param name="storedProcName"></param>
        /// <param name="parameters"></param>
        /// <param name="dataSet"></param>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public void RunProcedure(string storedProcName, IDataParameter[] parameters, DataSet dataSet, string tableName)
        {
            connection.Open();
            SqlDataAdapter sqlDA = new SqlDataAdapter();
            sqlDA.SelectCommand = BuildIntCommand(storedProcName, parameters);
            sqlDA.Fill(dataSet, tableName);
            connection.Close();
        }

        /// <summary>
        /// Creates a DataSet by running the query and placing the results
        /// of the query/proc into the given tablename.
        /// </summary>
        /// <param name="Query"></param>        
        /// <param name="tableName"></param>
        /// <returns>DataSet</returns>
        public DataSet RunQuery(string query, string tableName)
        {
            DataSet dataSet = new DataSet();
            connection.Open();
            SqlDataAdapter sqlDA = new SqlDataAdapter();

            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.CommandType = CommandType.Text;
            sqlDA.SelectCommand = cmd;
            sqlDA.Fill(dataSet, tableName);
            connection.Close();

            return dataSet;
        }
    }
}