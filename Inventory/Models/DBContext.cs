using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Inventory.DBContext
{
    public class InventoryCustomError : ApplicationException
    {
        public InventoryCustomError(string message) : base(message)
        {

        }
    }
    public class CustomVariables
    {
        public static string ConnectionString = "MDFConnection";

        public static string StrConnectionString()
        {
            string conndb = string.Empty;
            try
            {
                ConnectionStringSettingsCollection connstrings = ConfigurationManager.ConnectionStrings;
                string connstrvalue = ConnectionString;
                foreach (ConnectionStringSettings cs in connstrings)
                {
                    if (cs.Name.ToLower() == connstrvalue.ToLower())
                    {
                        conndb = cs.ConnectionString;
                    }
                }
                if (conndb == string.Empty)
                {
                    throw (new InventoryCustomError("Connection string '" + ConnectionString + "' not found in web.config file"));
                }
                else
                {
                    conndb = connstrvalue;
                }
                return conndb;
            }
            catch (Exception ex)
            {
                throw new ArgumentException("Error Occured in CustomVariables Error Message: " + ex.Message);
            }
        }

    }

    public class InventoryValuationDBContext : IDisposable
    {
        string strConndb = CustomVariables.StrConnectionString().ToString();
        string tablename = "Inventory_Valuation";
        public DataSet RetrieveAll()
        {
            SqlConnection connSQL = new SqlConnection();
            SqlCommand cmdSQL = new SqlCommand();
            SqlDataAdapter adapter = new SqlDataAdapter();
            DataSet ds = new DataSet();
            try
            {
                string conStr;
                conStr = ConfigurationManager.ConnectionStrings[strConndb].ConnectionString;
                connSQL = new SqlConnection(conStr);
                cmdSQL.Connection = connSQL;
                cmdSQL.CommandType = CommandType.StoredProcedure;
                cmdSQL.CommandText = "usp_Inventory_Valuation";
                cmdSQL.Parameters.AddWithValue("@action", "selectall");
                connSQL.Open();
                adapter = new SqlDataAdapter(cmdSQL);
                adapter.SelectCommand = cmdSQL;
                adapter.Fill(ds);
                return ds;
            }
            catch (Exception ex)
            {
                throw new ArgumentException("Error Occured in " + tablename + "." + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString() + " Error Message: " + ex.Message);
            }
            finally
            {
                if (ds != null)
                {
                    ds.Dispose();
                    ds = null;
                }

                if (adapter != null)
                {
                    adapter.Dispose();
                    adapter = null;
                }

                if (cmdSQL != null)
                {
                    cmdSQL.Dispose();
                    cmdSQL = null;
                }

                if (connSQL.State != ConnectionState.Closed | connSQL != null)
                {
                    connSQL.Close();
                    connSQL = null;
                }
            }
        }



        ~InventoryValuationDBContext()
        {
            Dispose(false);
        }
        protected void Dispose(bool disposing)
        {
            if (disposing)
            {
                strConndb = String.Empty;
                // dispose managed resources
            }
            // dispose unmanaged resources


        }
        #region IDisposable Members
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion
    }

    public class BacklogSummaryDBContext : IDisposable
    {
        string strConndb = CustomVariables.StrConnectionString().ToString();
        string tablename = "Backlog_Summary";
        public DataSet RetrieveAll()
        {
            SqlConnection connSQL = new SqlConnection();
            SqlCommand cmdSQL = new SqlCommand();
            SqlDataAdapter adapter = new SqlDataAdapter();
            DataSet ds = new DataSet();
            try
            {
                string conStr;
                conStr = ConfigurationManager.ConnectionStrings[strConndb].ConnectionString;
                connSQL = new SqlConnection(conStr);
                cmdSQL.Connection = connSQL;
                cmdSQL.CommandType = CommandType.StoredProcedure;
                cmdSQL.CommandText = "usp_Backlog_Summary";
                cmdSQL.Parameters.AddWithValue("@action", "selectall");
                connSQL.Open();
                adapter = new SqlDataAdapter(cmdSQL);
                adapter.SelectCommand = cmdSQL;
                adapter.Fill(ds);
                return ds;
            }
            catch (Exception ex)
            {
                throw new ArgumentException("Error Occured in " + tablename + "." + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString() + " Error Message: " + ex.Message);
            }
            finally
            {
                if (ds != null)
                {
                    ds.Dispose();
                    ds = null;
                }

                if (adapter != null)
                {
                    adapter.Dispose();
                    adapter = null;
                }

                if (cmdSQL != null)
                {
                    cmdSQL.Dispose();
                    cmdSQL = null;
                }

                if (connSQL.State != ConnectionState.Closed | connSQL != null)
                {
                    connSQL.Close();
                    connSQL = null;
                }
            }
        }



        ~BacklogSummaryDBContext()
        {
            Dispose(false);
        }
        protected void Dispose(bool disposing)
        {
            if (disposing)
            {
                strConndb = String.Empty;
                // dispose managed resources
            }
            // dispose unmanaged resources


        }
        #region IDisposable Members
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion
    }

    public class BacklogDetailDBContext : IDisposable
    {
        string strConndb = CustomVariables.StrConnectionString().ToString();
        string tablename = "Backlog_Detail";
        public DataSet RetrieveAll()
        {
            SqlConnection connSQL = new SqlConnection();
            SqlCommand cmdSQL = new SqlCommand();
            SqlDataAdapter adapter = new SqlDataAdapter();
            DataSet ds = new DataSet();
            try
            {
                string conStr;
                conStr = ConfigurationManager.ConnectionStrings[strConndb].ConnectionString;
                connSQL = new SqlConnection(conStr);
                cmdSQL.Connection = connSQL;
                cmdSQL.CommandType = CommandType.StoredProcedure;
                cmdSQL.CommandText = "usp_Backlog_Detail";
                cmdSQL.Parameters.AddWithValue("@action", "selectall");
                connSQL.Open();
                adapter = new SqlDataAdapter(cmdSQL);
                adapter.SelectCommand = cmdSQL;
                adapter.Fill(ds);
                return ds;
            }
            catch (Exception ex)
            {
                throw new ArgumentException("Error Occured in " + tablename + "." + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString() + " Error Message: " + ex.Message);
            }
            finally
            {
                if (ds != null)
                {
                    ds.Dispose();
                    ds = null;
                }

                if (adapter != null)
                {
                    adapter.Dispose();
                    adapter = null;
                }

                if (cmdSQL != null)
                {
                    cmdSQL.Dispose();
                    cmdSQL = null;
                }

                if (connSQL.State != ConnectionState.Closed | connSQL != null)
                {
                    connSQL.Close();
                    connSQL = null;
                }
            }
        }

        ~BacklogDetailDBContext()
        {
            Dispose(false);
        }
        protected void Dispose(bool disposing)
        {
            if (disposing)
            {
                strConndb = String.Empty;
                // dispose managed resources
            }
            // dispose unmanaged resources


        }
        #region IDisposable Members
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}