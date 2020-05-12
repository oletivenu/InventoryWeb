using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using System.IO;
using Inventory.Models;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;

namespace Inventory.Controllers.forms
{
    public class ListController : Controller
    {
        // GET: List
        public ActionResult Index()
        {
            ViewBag.EmployeeData = EmployeeModel.GetEmployees();
            ViewBag.Inventory_Valuation_Data = ModelList.Get_Inventory_Valuations();
            return View();
        }
        [HttpPost]
        public ActionResult Index(HttpPostedFileBase postedFile)
        {
            if (postedFile == null || postedFile.ContentLength == 0)
            {
                ViewBag.error = "Please select a excel file<br/>";
                return View("Index");
            }
            else
            {
                if (postedFile.FileName.EndsWith("xls") || postedFile.FileName.EndsWith("xlsx"))
                {
                    string path = Server.MapPath("~/Uploads/");
                    string filePath = string.Empty;
                    string conString = string.Empty;
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }
                    filePath = path + Path.GetFileName(postedFile.FileName);
                    string extension = Path.GetExtension(postedFile.FileName);
                    if (System.IO.File.Exists(filePath))
                    {
                        System.IO.File.Delete(filePath);
                    }
                    postedFile.SaveAs(filePath);
                    DataTable dt = new DataTable();
                    dt = Common.ExceltoDataTable(filePath, extension);

                    conString = ConfigurationManager.ConnectionStrings["MDFConnection"].ConnectionString;
                    using (SqlConnection con = new SqlConnection(conString))
                    {
                        using (SqlBulkCopy sqlBulkCopy = new SqlBulkCopy(con))
                        {
                            //Set the database table name.
                            sqlBulkCopy.DestinationTableName = "dbo.Inventory_Valuation";

                            //[OPTIONAL]: Map the Excel columns with that of the database table
                            sqlBulkCopy.ColumnMappings.Add("Inv Org", "Inv_Org");
                            sqlBulkCopy.ColumnMappings.Add("Item_Number", "Item_Number");
                            sqlBulkCopy.ColumnMappings.Add("Item_Description", "Item_Description");
                            sqlBulkCopy.ColumnMappings.Add("Subinv", "Subinv");
                            sqlBulkCopy.ColumnMappings.Add("WIP Subinv", "WIP_Subinv");
                            sqlBulkCopy.ColumnMappings.Add("User Item Type", "User_Item_Type");
                            sqlBulkCopy.ColumnMappings.Add("Planner_Code", "Planner_Code");
                            sqlBulkCopy.ColumnMappings.Add("Purch Item", "Purch_Item");
                            sqlBulkCopy.ColumnMappings.Add("Bus Area", "Bus_Area");
                            sqlBulkCopy.ColumnMappings.Add("Prod Cat", "Prod_Cat");
                            sqlBulkCopy.ColumnMappings.Add("Prod Family", "Prod_Family");
                            sqlBulkCopy.ColumnMappings.Add("Prod Type", "Prod_Type");
                            sqlBulkCopy.ColumnMappings.Add("Primary UOM", "Primary_UOM");
                            sqlBulkCopy.ColumnMappings.Add("Onhand Quantity", "Onhand_Quantity");
                            sqlBulkCopy.ColumnMappings.Add("Unit Cost @ Std", "Unit_Cost_Std");
                            sqlBulkCopy.ColumnMappings.Add("Ext'd Value", "Extd_Value");

                            con.Open();
                            sqlBulkCopy.WriteToServer(dt);
                            con.Close();
                        }
                    }
                    ViewBag.error = "File uploaded successfully<br/>";
                    //return View();
                    return RedirectToAction("Index", "List");
                }
                else
                {
                    ViewBag.error = "File type is incorrect<br/>";
                    return View("Index");
                }
            }
        }

        public ActionResult BacklogSummary()
        {
            ViewBag.Backlog_Summary_Data = ModelList.Get_Backlog_Summary();
            return View();
        }

        [HttpPost]
        public ActionResult BacklogSummary(HttpPostedFileBase postedFile)
        {
            if (postedFile == null || postedFile.ContentLength == 0)
            {
                ViewBag.error = "Please select a excel file<br/>";
                return View("Index");
            }
            else
            {
                if (postedFile.FileName.EndsWith("xls") || postedFile.FileName.EndsWith("xlsx"))
                {
                    string path = Server.MapPath("~/Uploads/");
                    string filePath = string.Empty;
                    string conString = string.Empty;
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }
                    filePath = path + Path.GetFileName(postedFile.FileName);
                    string extension = Path.GetExtension(postedFile.FileName);
                    //postedFile.SaveAs(filePath);
                    if (System.IO.File.Exists(filePath))
                    {
                        System.IO.File.Delete(filePath);
                    }
                    postedFile.SaveAs(filePath);
                    DataTable dt = new DataTable();
                    dt = Common.ExceltoDataTable(filePath, extension);
                    
                    conString = ConfigurationManager.ConnectionStrings["MDFConnection"].ConnectionString;
                    using (SqlConnection con = new SqlConnection(conString))
                    {
                        using (SqlBulkCopy sqlBulkCopy = new SqlBulkCopy(con))
                        {
                            //Set the database table name.
                            sqlBulkCopy.DestinationTableName = "dbo.Backlog_Summary";

                            //[OPTIONAL]: Map the Excel columns with that of the database table
                            sqlBulkCopy.ColumnMappings.Add("Oper Unit", "Oper_Unit");
                            sqlBulkCopy.ColumnMappings.Add("Order No", "Order_No");
                            sqlBulkCopy.ColumnMappings.Add("Creation Date", "Creation_Date");
                            sqlBulkCopy.ColumnMappings.Add("Customer Name", "Customer_Name");
                            sqlBulkCopy.ColumnMappings.Add("Current_Request_Date", "Current_Request_Date");
                            sqlBulkCopy.ColumnMappings.Add("Current Ship Date", "Current_Ship_Date");
                            sqlBulkCopy.ColumnMappings.Add("Required_Build_Date", "Required_Build_Date");
                            sqlBulkCopy.ColumnMappings.Add("Extended Material Cost in USD", "Extended_Material_Cost_USD");
                            sqlBulkCopy.ColumnMappings.Add("Line Total In Usd SUM", "Line_Total_USD_SUM");
                            sqlBulkCopy.ColumnMappings.Add("Ip Order Subtype", "IP_Order_Subtype");
                            sqlBulkCopy.ColumnMappings.Add("Project Substation Name", "Project_Substation_Name");
                            sqlBulkCopy.ColumnMappings.Add("Freight Terms Header", "Freight_Terms_Header");
                            sqlBulkCopy.ColumnMappings.Add("Incoterm", "Incoterm");
                            sqlBulkCopy.ColumnMappings.Add("Business Area", "Business_Area");
                            sqlBulkCopy.ColumnMappings.Add("Ip Process Owner", "IP_Process_Owner");

                            con.Open();
                            sqlBulkCopy.WriteToServer(dt);
                            con.Close();
                        }
                    }

                    //return View();
                    return RedirectToAction("BacklogSummary", "List");
                }
                else
                {
                    ViewBag.error = "File type is incorrect<br/>";
                    return View();
                }
            }
        }

        public ActionResult BacklogDetail()
        {
            ViewBag.Backlog_Detail_Data = ModelList.Get_Backlog_Detail();
            return View();
        }

        [HttpPost]
        public ActionResult BacklogDetail(HttpPostedFileBase postedFile)
        {
            if (postedFile == null || postedFile.ContentLength == 0)
            {
                ViewBag.error = "Please select a excel file<br/>";
                return View();
            }
            else
            {
                if (postedFile.FileName.EndsWith("xls") || postedFile.FileName.EndsWith("xlsx"))
                {
                    string path = Server.MapPath("~/Uploads/");
                    string filePath = string.Empty;
                    string conString = string.Empty;
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }
                    filePath = path + Path.GetFileName(postedFile.FileName);
                    string extension = Path.GetExtension(postedFile.FileName);
                    //postedFile.SaveAs(filePath);
                    if (System.IO.File.Exists(filePath))
                    {
                        System.IO.File.Delete(filePath);
                    }
                    postedFile.SaveAs(filePath);
                    DataTable dt = new DataTable();
                    dt = Common.ExceltoDataTable(filePath, extension);

                    conString = ConfigurationManager.ConnectionStrings["MDFConnection"].ConnectionString;
                    using (SqlConnection con = new SqlConnection(conString))
                    {
                        using (SqlBulkCopy sqlBulkCopy = new SqlBulkCopy(con))
                        {
                            //Set the database table name.
                            sqlBulkCopy.DestinationTableName = "dbo.Backlog_Detail";

                            //[OPTIONAL]: Map the Excel columns with that of the database table
                            sqlBulkCopy.ColumnMappings.Add("Oper Unit", "Oper_Unit");
                            sqlBulkCopy.ColumnMappings.Add("Mfg Plant", "Mfg_Plant");
                            sqlBulkCopy.ColumnMappings.Add("Order No", "Order_No");
                            sqlBulkCopy.ColumnMappings.Add("Customer PO Number", "Customer_PO_Number");
                            sqlBulkCopy.ColumnMappings.Add("Customer Name", "Customer_Name");
                            sqlBulkCopy.ColumnMappings.Add("Ship Customer Name", "Ship_Customer_Name");
                            sqlBulkCopy.ColumnMappings.Add("Request Date", "Request_Date");
                            sqlBulkCopy.ColumnMappings.Add("Current Ship Date", "Current_Ship_Date");
                            sqlBulkCopy.ColumnMappings.Add("Required_Build_Date", "Required_Build_Date");
                            sqlBulkCopy.ColumnMappings.Add("Line Status", "Line_Status");
                            sqlBulkCopy.ColumnMappings.Add("Item No", "Item_No");
                            sqlBulkCopy.ColumnMappings.Add("Quantity", "Quantity");
                            sqlBulkCopy.ColumnMappings.Add("Unit Cost", "Unit_Cost");
                            sqlBulkCopy.ColumnMappings.Add("Extended Material Cost in Building Currency", "Extended_Material_Cost_Building_Currency");
                            sqlBulkCopy.ColumnMappings.Add("Line Total in Func Curr", "Line_Total_Func_Curr");
                            sqlBulkCopy.ColumnMappings.Add("Line Total In Usd", "Line_Total_USD");
                            sqlBulkCopy.ColumnMappings.Add("Incoterm", "Incoterm");
                            sqlBulkCopy.ColumnMappings.Add("Payment Terms", "Payment_Terms");
                            sqlBulkCopy.ColumnMappings.Add("Item_Type_Code", "Item_Type_Code");
                            sqlBulkCopy.ColumnMappings.Add("Business Area", "Business_Area");
                            sqlBulkCopy.ColumnMappings.Add("Latest_USD_Exchange_Rate", "Latest_USD_Exchange_Rate");
                            sqlBulkCopy.ColumnMappings.Add("Material_Cost_USD", "Material_Cost_USD");
                            sqlBulkCopy.ColumnMappings.Add("Extended Material Cost in USD", "Extended_Material_Cost_USD");
                            sqlBulkCopy.ColumnMappings.Add("Project Substation Name", "Project_Substation_Name");

                            con.Open();
                            sqlBulkCopy.WriteToServer(dt);
                            con.Close();
                        }
                    }
                    ViewBag.error = "File uploaded successfully<br/>";
                    //return View();
                    return RedirectToAction("BacklogDetail", "List");
                }
                else
                {
                    ViewBag.error = "File type is incorrect<br/>";
                    return View();
                }
            }
        }

        //public ActionResult GetData(JqueryDatatableParam param)
        //{
        //    TypeFormDetails td = new TypeFormDetails();

        //    var employees = EmployeeModel.GetEmployees();

        //    employees.ToList().ForEach(x => x.StartDateString = x.StartDate.ToString("dd'/'MM'/'yyyy"));

        //    if (!string.IsNullOrEmpty(param.sSearch))
        //    {
        //        employees = employees.Where(x => x.Name.ToLower().Contains(param.sSearch.ToLower())
        //                                      || x.Position.ToLower().Contains(param.sSearch.ToLower())
        //                                      || x.Location.ToLower().Contains(param.sSearch.ToLower())
        //                                      || x.Salary.ToString().Contains(param.sSearch.ToLower())
        //                                      || x.Age.ToString().Contains(param.sSearch.ToLower())
        //                                      || x.StartDate.ToString("dd'/'MM'/'yyyy").ToLower().Contains(param.sSearch.ToLower())).ToList();
        //    }

        //    var sortColumnIndex = Convert.ToInt32(HttpContext.Request.QueryString["iSortCol_0"]);
        //    var sortDirection = HttpContext.Request.QueryString["sSortDir_0"];

        //    if (sortColumnIndex == 3)
        //    {
        //        employees = sortDirection == "asc" ? employees.OrderBy(c => c.Age) : employees.OrderByDescending(c => c.Age);
        //    }
        //    else if (sortColumnIndex == 4)
        //    {
        //        employees = sortDirection == "asc" ? employees.OrderBy(c => c.StartDate) : employees.OrderByDescending(c => c.StartDate);
        //    }
        //    else if (sortColumnIndex == 5)
        //    {
        //        employees = sortDirection == "asc" ? employees.OrderBy(c => c.Salary) : employees.OrderByDescending(c => c.Salary);
        //    }
        //    else
        //    {
        //        Func<Employee, string> orderingFunction = e => sortColumnIndex == 0 ? e.Name :
        //                                                       sortColumnIndex == 1 ? e.Position :
        //                                                       e.Location;

        //        employees = sortDirection == "asc" ? employees.OrderBy(orderingFunction) : employees.OrderByDescending(orderingFunction);
        //    }

        //    var displayResult = employees.Skip(param.iDisplayStart)
        //        .Take(param.iDisplayLength).ToList();
        //    var totalRecords = employees.Count();

        //    return Json(new
        //    {
        //        param.sEcho,
        //        iTotalRecords = totalRecords,
        //        iTotalDisplayRecords = totalRecords,
        //        aaData = displayResult
        //    }, JsonRequestBehavior.AllowGet);

        //}
    }
}