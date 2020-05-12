using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;


namespace Inventory.Models
{
    public class Inventory
    {
        
    }

    public class EmployeeModel {

        public static List<Employee> GetEmployees()
        {
            SQLHelper dbo = new SQLHelper();
            DataSet ds = null;
            List<Employee> listModel = new List<Employee>();
            try
            {
                SqlParameter[] parameters = new SqlParameter[]
                {
                };
                ds = dbo.RunProcedure("usp_employeedata", parameters, "EmployeeData");
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0] != null)
                {
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        Employee model = new Employee();
                        model.Id = Convert.ToInt32(dr["Id"]);
                        model.Name = Convert.ToString(dr["Name"]);
                        model.Position = Convert.ToString(dr["Position"]);
                        model.Location = Convert.ToString(dr["Location"]);
                        model.Age = Convert.ToInt32(dr["Age"]);
                        model.StartDate = Convert.ToDateTime(dr["StartDate"]);
                        model.Salary = Convert.ToInt32(dr["salary"]);
                        listModel.Add(model);
                    }
                }
                return listModel;
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
    
    public class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Position { get; set; }
        public string Location { get; set; }
        public int Age { get; set; }
        public DateTime StartDate { get; set; }
        public string StartDateString { get; set; }
        public int Salary { get; set; }
    }
    public class JqueryDatatableParam
    {
        public string sEcho { get; set; }
        public string sSearch { get; set; }
        public int iDisplayLength { get; set; }
        public int iDisplayStart { get; set; }
        public int iColumns { get; set; }
        public int iSortCol_0 { get; set; }
        public string sSortDir_0 { get; set; }
        public int iSortingCols { get; set; }
        public string sColumns { get; set; }
    }
    public class Inventory_Valuation
    {
        public int Id { get; set; }
        public string Inv_Org { get; set; }
        public string Item_Number { get; set; }
        public string Item_Description { get; set; }
        public string Subinv { get; set; }
        public string WIP_Subinv { get; set; }
        public string User_Item_Type { get; set; }
        public string Planner_Code { get; set; }
        public string Purch_Item { get; set; }
        public string Bus_Area { get; set; }
        public string Prod_Cat { get; set; }
        public string Prod_Family { get; set; }
        public string Prod_Type { get; set; }
        public string Primary_UOM { get; set; }
        public string Onhand_Quantity { get; set; }
        public string Unit_Cost_Std { get; set; }
        public string Extd_Value { get; set; }
        public DateTime Created_Date { get; set; }
    }

    public class Backlog_Summary
    {
        public int Id { get; set; }
        public string Oper_Unit { get; set; }
        public string Order_No { get; set; }
        public DateTime? Creation_Date { get; set; }
        public string Customer_Name { get; set; }
        public DateTime? Current_Request_Date { get; set; }
        public DateTime? Current_Ship_Date { get; set; }
        public DateTime? Required_Build_Date { get; set; }
        public string Extended_Material_Cost_USD { get; set; }
        public string Line_Total_USD_SUM { get; set; }
        public string IP_Order_Subtype { get; set; }
        public string Project_Substation_Name { get; set; }
        public string Freight_Terms_Header { get; set; }
        public string Incoterm { get; set; }
        public string Business_Area { get; set; }
        public string IP_Process_Owner { get; set; }
        public DateTime Created_Date { get; set; }

    }

    public class Backlog_Detail
    {
        public int Id { get; set; }
        public string Oper_Unit { get; set; }
        public string Mfg_Plant { get; set; }
        public string Order_No { get; set; }
        public string Customer_PO_Number { get; set; }
        public string Customer_Name { get; set; }
        public string Ship_Customer_Name { get; set; }
        public DateTime? Request_Date { get; set; }
        public DateTime? Current_Ship_Date { get; set; }
        public DateTime? Required_Build_Date { get; set; }
        public string Line_Status { get; set; }
        public string Item_No { get; set; }
        public string Quantity { get; set; }
        public string Unit_Cost { get; set; }
        public string Extended_Material_Cost_Building_Currency { get; set; }
        public string Line_Total_Func_Curr { get; set; }
        public string Line_Total_USD { get; set; }
        public string Incoterm { get; set; }
        public string Payment_Terms { get; set; }
        public string Item_Type_Code { get; set; }
        public string Business_Area { get; set; }
        public string Latest_USD_Exchange_Rate { get; set; }
        public string Material_Cost_USD { get; set; }
        public string Extended_Material_Cost_USD { get; set; }
        public string Project_Substation_Name { get; set; }
        public DateTime Created_Date { get; set; }
    }
}