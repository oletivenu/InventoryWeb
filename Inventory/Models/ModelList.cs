using Inventory.DBContext;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace Inventory.Models
{
    public partial class ModelList
    {
        public static List<Inventory_Valuation> Get_Inventory_Valuations()
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            List<Inventory_Valuation> listmodel = new List<Inventory_Valuation>();
            InventoryValuationDBContext dbContext = new InventoryValuationDBContext();
            try
            {
                if (dbContext != null)
                {
                    if (null != listmodel)
                    {
                        ds = dbContext.RetrieveAll();

                        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0] != null)
                        {
                            foreach (DataRow dr in ds.Tables[0].Rows)
                            {
                                Inventory_Valuation model = new Inventory_Valuation();
                                model.Id = Convert.ToInt32(dr["Id"]);
                                model.Inv_Org = Convert.ToString(dr["Inv_Org"]);
                                model.Item_Number = Convert.ToString(dr["Item_Number"]);
                                model.Item_Description = Convert.ToString(dr["Item_Description"]);
                                model.Subinv = Convert.ToString(dr["Subinv"]);
                                model.WIP_Subinv = Convert.ToString(dr["WIP_Subinv"]);
                                model.User_Item_Type = Convert.ToString(dr["User_Item_Type"]);
                                model.Planner_Code = Convert.ToString(dr["Planner_Code"]);
                                model.Purch_Item = Convert.ToString(dr["Purch_Item"]);
                                model.Bus_Area = Convert.ToString(dr["Bus_Area"]);
                                model.Prod_Cat = Convert.ToString(dr["Prod_Cat"]);
                                model.Prod_Family = Convert.ToString(dr["Prod_Family"]);
                                model.Prod_Type = Convert.ToString(dr["Prod_Type"]);
                                model.Primary_UOM = Convert.ToString(dr["Primary_UOM"]);
                                model.Onhand_Quantity = Convert.ToString(dr["Onhand_Quantity"]);
                                model.Unit_Cost_Std = Convert.ToString(dr["Unit_Cost_Std"]);
                                model.Extd_Value = Convert.ToString(dr["Extd_Value"]);
                                model.Created_Date = Convert.ToDateTime(dr["Created_Date"]);
                                listmodel.Add(model);
                            }
                        }

                    }
                }
                return listmodel;
            }
            catch (Exception ex)
            {
                throw new ArgumentException("Error Occured in " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString() + " Error Message: " + ex.Message);
            }
            finally
            {

                if (dt != null)
                {
                    dt.Dispose();
                    dt = null;
                }

                if (listmodel != null)
                {
                    listmodel = null;
                }

                if (null != dbContext)
                {
                    dbContext.Dispose();
                    dbContext = null;
                }
            }
        }

        public static List<Backlog_Summary> Get_Backlog_Summary()
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            List<Backlog_Summary> listmodel = new List<Backlog_Summary>();
            BacklogSummaryDBContext dbContext = new BacklogSummaryDBContext();
            try
            {
                if (dbContext != null)
                {
                    if (null != listmodel)
                    {
                        ds = dbContext.RetrieveAll();

                        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0] != null)
                        {
                            foreach (DataRow dr in ds.Tables[0].Rows)
                            {
                                Backlog_Summary model = new Backlog_Summary();
                                model.Id = Convert.ToInt32(dr["Id"]);
                                model.Oper_Unit = Convert.ToString(dr["Oper_Unit"]);
                                model.Order_No = Convert.ToString(dr["Order_No"]);
                                if (dr["Creation_Date"] != DBNull.Value)
                                {
                                    model.Creation_Date = Convert.ToDateTime(dr["Creation_Date"]);
                                }
                                else
                                {
                                    model.Creation_Date = null;
                                }

                                model.Customer_Name = Convert.ToString(dr["Customer_Name"]);
                                if (dr["Current_Request_Date"] != DBNull.Value)
                                {
                                    model.Current_Request_Date = Convert.ToDateTime(dr["Current_Request_Date"]);
                                }
                                else
                                {
                                    model.Current_Request_Date = null;
                                }
                                if (dr["Current_Ship_Date"] != DBNull.Value)
                                {
                                    model.Current_Ship_Date = Convert.ToDateTime(dr["Current_Ship_Date"]);
                                }
                                else
                                {
                                    model.Current_Ship_Date = null;
                                }
                                if (dr["Required_Build_Date"] != DBNull.Value)
                                {
                                    model.Required_Build_Date = Convert.ToDateTime(dr["Required_Build_Date"]);
                                }
                                else
                                {
                                    model.Required_Build_Date = null;
                                }
                                
                                model.Extended_Material_Cost_USD = Convert.ToString(dr["Extended_Material_Cost_USD"]);
                                model.Line_Total_USD_SUM = Convert.ToString(dr["Line_Total_USD_SUM"]);
                                model.IP_Order_Subtype = Convert.ToString(dr["IP_Order_Subtype"]);
                                model.Project_Substation_Name = Convert.ToString(dr["Project_Substation_Name"]);
                                model.Freight_Terms_Header = Convert.ToString(dr["Freight_Terms_Header"]);
                                model.Incoterm = Convert.ToString(dr["Incoterm"]);
                                model.Business_Area = Convert.ToString(dr["Business_Area"]);
                                model.IP_Process_Owner = Convert.ToString(dr["IP_Process_Owner"]);
                                model.Created_Date = Convert.ToDateTime(dr["Created_Date"]);
                                listmodel.Add(model);
                            }
                        }

                    }
                }
                return listmodel;
            }
            catch (Exception ex)
            {
                throw new ArgumentException("Error Occured in " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString() + " Error Message: " + ex.Message);
            }
            finally
            {

                if (dt != null)
                {
                    dt.Dispose();
                    dt = null;
                }

                if (listmodel != null)
                {
                    listmodel = null;
                }

                if (null != dbContext)
                {
                    dbContext.Dispose();
                    dbContext = null;
                }
            }
        }

        public static List<Backlog_Detail> Get_Backlog_Detail()
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            List<Backlog_Detail> listmodel = new List<Backlog_Detail>();
            BacklogDetailDBContext dbContext = new BacklogDetailDBContext();
            try
            {
                if (dbContext != null)
                {
                    if (null != listmodel)
                    {
                        ds = dbContext.RetrieveAll();

                        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0] != null)
                        {
                            foreach (DataRow dr in ds.Tables[0].Rows)
                            {
                                Backlog_Detail model = new Backlog_Detail();
                                model.Id = Convert.ToInt32(dr["Id"]);
                                model.Oper_Unit = Convert.ToString(dr["Oper_Unit"]);
                                model.Mfg_Plant = Convert.ToString(dr["Mfg_Plant"]);
                                model.Order_No = Convert.ToString(dr["Order_No"]);
                                model.Customer_PO_Number = Convert.ToString(dr["Customer_PO_Number"]);
                                model.Customer_Name = Convert.ToString(dr["Customer_Name"]);
                                model.Ship_Customer_Name = Convert.ToString(dr["Ship_Customer_Name"]);
                                if (dr["Request_Date"] != DBNull.Value)
                                {
                                    model.Request_Date = Convert.ToDateTime(dr["Request_Date"]);
                                }
                                else {
                                    model.Request_Date = null;
                                }
                                if (dr["Current_Ship_Date"] != DBNull.Value)
                                {
                                    model.Current_Ship_Date = Convert.ToDateTime(dr["Current_Ship_Date"]);
                                }
                                else
                                {
                                    model.Current_Ship_Date = null;
                                }
                                if (dr["Required_Build_Date"] != DBNull.Value)
                                {
                                    model.Required_Build_Date = Convert.ToDateTime(dr["Required_Build_Date"]);
                                }
                                else
                                {
                                    model.Required_Build_Date = null;
                                }
                                model.Line_Status = Convert.ToString(dr["Line_Status"]);
                                model.Item_No = Convert.ToString(dr["Item_No"]);
                                model.Quantity = Convert.ToString(dr["Quantity"]);
                                model.Unit_Cost = Convert.ToString(dr["Unit_Cost"]);
                                model.Extended_Material_Cost_Building_Currency = Convert.ToString(dr["Extended_Material_Cost_Building_Currency"]);
                                model.Line_Total_Func_Curr = Convert.ToString(dr["Line_Total_Func_Curr"]);
                                model.Line_Total_USD = Convert.ToString(dr["Line_Total_USD"]);
                                model.Incoterm = Convert.ToString(dr["Incoterm"]);
                                model.Payment_Terms = Convert.ToString(dr["Payment_Terms"]);
                                model.Item_Type_Code = Convert.ToString(dr["Item_Type_Code"]);
                                model.Business_Area = Convert.ToString(dr["Business_Area"]);
                                model.Latest_USD_Exchange_Rate = Convert.ToString(dr["Latest_USD_Exchange_Rate"]);
                                model.Material_Cost_USD = Convert.ToString(dr["Material_Cost_USD"]);
                                model.Extended_Material_Cost_USD = Convert.ToString(dr["Extended_Material_Cost_USD"]);
                                model.Project_Substation_Name = Convert.ToString(dr["Project_Substation_Name"]);
                                model.Created_Date = Convert.ToDateTime(dr["Created_Date"]);
                                listmodel.Add(model);
                            }
                        }

                    }
                }
                return listmodel;
            }
            catch (Exception ex)
            {
                throw new ArgumentException("Error Occured in " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString() + " Error Message: " + ex.Message);
            }
            finally
            {

                if (dt != null)
                {
                    dt.Dispose();
                    dt = null;
                }

                if (listmodel != null)
                {
                    listmodel = null;
                }

                if (null != dbContext)
                {
                    dbContext.Dispose();
                    dbContext = null;
                }
            }
        }
    }
    
}