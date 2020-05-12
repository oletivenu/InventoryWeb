using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;


namespace Inventory.Models
{
    public class companymethodmodel
    {
        public string DatatableToJson(DataTable dt)
        {
            System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
            Dictionary<string, object> row;
            foreach (DataRow dr in dt.Rows)
            {
                row = new Dictionary<string, object>();
                foreach (DataColumn col in dt.Columns)
                {
                    row.Add(col.ColumnName, dr[col]);
                }
                rows.Add(row);
            }
            return serializer.Serialize(rows);
        }
        
        public string GetCompanyDetails(string license_no)
        {
            SQLHelper dbo = new SQLHelper();
            DataSet ds = null;

            try
            {
                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("license_no",license_no),
                };

                ds = dbo.RunProcedure("usp_Company_Details", parameters, "usp_Company_Details");

                if (ds != null && ds.Tables[0] != null)
                {
                    return DatatableToJson(ds.Tables[0]);
                }

                return null;
            }
            catch (Exception ex)
            {
                return null;
            }
            finally
            {
                dbo = null;

            }
        }
    }
}