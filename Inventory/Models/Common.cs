using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using Inventory.Models;

namespace Inventory.Models
{
    public class Common
    {
        public static DataTable ExceltoDataTable(string filePath,string extension)
        {
            string conString = string.Empty;
            switch (extension)
            {
                case ".xls": //Excel 97-03.
                    conString = ConfigurationManager.ConnectionStrings["Excel03ConString"].ConnectionString;
                    break;
                case ".xlsx": //Excel 07 and above.
                    conString = ConfigurationManager.ConnectionStrings["Excel07ConString"].ConnectionString;
                    break;
            }
            DataTable dt = new DataTable();
            conString = string.Format(conString, filePath);
            using (OleDbConnection connExcel = new OleDbConnection(conString))
            {
                using (OleDbCommand cmdExcel = new OleDbCommand())
                {
                    using (OleDbDataAdapter odaExcel = new OleDbDataAdapter())
                    {
                        cmdExcel.Connection = connExcel;

                        //Get the name of First Sheet.
                        connExcel.Open();
                        //string tableName = connExcel.GetSchema("Tables").Rows[0]["TABLE_NAME"].ToString();
                        DataTable dtExcelSchema;
                        dtExcelSchema = connExcel.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                        string tableName = dtExcelSchema.Rows[0]["TABLE_NAME"].ToString();
                        connExcel.Close();

                        //Read Data from First Sheet.
                        connExcel.Open();
                        cmdExcel.CommandText = "SELECT * From [" + tableName + "]";
                        odaExcel.SelectCommand = cmdExcel;
                        odaExcel.Fill(dt);
                        connExcel.Close();
                    }
                }
            }
            return dt;
        }
    }
}