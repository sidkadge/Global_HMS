using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebApplication1.Models;
using System.Globalization;

namespace WebApplication1.Controllers
{
    public class IPDController : Controller
    {
        private SqlConnection con;
        string conString = ConfigurationManager.ConnectionStrings["Connection"].ToString();
        //string conString = ConfigurationSettings.AppSettings("Connection");
        private void connection()
        {
            con = new SqlConnection(conString);
        }
        IPDDBHandler IPDDB = new IPDDBHandler();

        public object IPDBillinglistDateList { get; private set; }

        // GET: IPD
        public ActionResult IPDPatientDetailsIndex(string searchBy, string SearchValue, DateTime? Fromdate, DateTime? Todate)
        {
            var IPDPatientDetails = IPDDB.GetIPDPatientDetails();

            if (Fromdate != null && Todate != null)
            {


                //string fdate = Fromdate;
                //string tdate = Todate;
                string formattedDate = Fromdate.Value.ToString("yyyy-MM-dd");
                string formattedToDate = Todate.Value.ToString("yyyy-MM-dd");
              
                connection();
                List<IPDModel> IPDPatientDetailDates = new List<IPDModel>();
                string sqlQuery = @"SELECT Branch, Patient_ID_sr, IPD_ID_Sr, Name, Doctor_name, Admission_Date, Discharge_Date FROM HMS_Global.IPD_Patient_Details  where Admission_Date BETWEEN  '" + formattedDate + "'and'" + formattedToDate + "'";
                SqlCommand command = new SqlCommand(sqlQuery, con);
                con.Open();
                SqlDataAdapter sd = new SqlDataAdapter(command);
                DataTable dt = new DataTable();
                sd.Fill(dt);
                con.Close();
               
                foreach (DataRow dr in dt.Rows)
                {
                    IPDPatientDetailDates.Add(new IPDModel
                    {

                        Branch = dr["Branch"].ToString(),
                        Patient_ID_sr = dr["Patient_ID_sr"].ToString(),
                        IPD_ID_Sr = dr["IPD_ID_Sr"].ToString(),
                        Name = dr["Name"].ToString(),
                        Doctor_name = dr["Doctor_name"].ToString(),
                        Admission_Date = Convert.ToDateTime(dr["Admission_Date"]),
                       // Discharge_Date = Convert.ToDateTime(dr["Discharge_Date"]),
                        Discharge_Date=dr["Discharge_Date"] != DBNull.Value? (DateTime?)Convert.ToDateTime(dr["Discharge_Date"]): (DateTime?)null,


                });
                }
                if (SearchValue == "All")
                {
                    return View(IPDPatientDetailDates);
                }
                else
                {
                    var Searchbyproductname = IPDPatientDetailDates.Where(p => p.Branch.ToLower().Contains(SearchValue.ToLower()));
                    return View(Searchbyproductname);
                }

            }
            else
            {
                return View(IPDPatientDetails);
            }

            return View(IPDPatientDetails);
            
        }
        public ActionResult IPDBillingIndex(string searchBy, string SearchValue, string SearchBillPAyment, DateTime? Fromdate, DateTime? Todate)
        {
            var IPDBillinglist = IPDDB.GetIPDBillings();

            ViewBag.ErrorMsg = string.Empty;

            if (Fromdate != null && Todate != null)
            {
                //string formattedDate = Convert.ToString(Fromdate);
                //DateTime originalDateTime = DateTime.ParseExact(formattedDate.Trim('{', '}'), "dd-MM-yyyy hh:mm:ss tt", System.Globalization.CultureInfo.InvariantCulture);
                string fdate = Fromdate.Value.ToString("yyyy-MM-dd");

                //string formattedToDate = Convert.ToString(Todate);
                //DateTime originalToDateTime = DateTime.ParseExact(formattedToDate.Trim('{', '}'), "dd-MM-yyyy hh:mm:ss tt", System.Globalization.CultureInfo.InvariantCulture);
                string tdate = Todate.Value.ToString("yyyy-MM-dd");
                
                connection();
                List<IPDModel> IPDBillinglistDate = new List<IPDModel>();
                //IPDBillinglistDate = IPDBillinglistDate ?? new List<IPDModel>();
                //change
                if (SearchValue == "All" && SearchBillPAyment=="All")
                {
                    string sqlQuery = @"SELECT  GID, Branch, PaymentMode, Patient_ID_sr, Name, Doctor_name, Billing_date, Bill_Amount, Discount, Net_Payable, Received, Pending, Hospital_Total, Surgical_Total, Medical_Record_Charges, BioMedical_Waste_Charges, 
                        Administrative_Charges, Consultant_Visiting_Charges, Nursing_Charges, Bed_Charges, Lab_Total, Radiology_Total, Registration_Charges, Consultant_Charges FROM  HMS_Global.IPD_Patient_Details where Billing_date BETWEEN   '" + fdate + "'and'" + tdate + "'";
                    SqlCommand command = new SqlCommand(sqlQuery, con);
                    con.Open();
                    SqlDataAdapter sd = new SqlDataAdapter(command);
                    DataTable dt = new DataTable();
                    sd.Fill(dt);
                    con.Close();
                    foreach (DataRow dr in dt.Rows)
                    {
                        IPDBillinglistDate.Add(new IPDModel
                        {
                            GID = Convert.ToInt32(dr["GID"]),
                            Branch = dr["Branch"].ToString(),
                            PaymentMode = dr["PaymentMode"].ToString(),
                            Patient_ID_sr = dr["Patient_ID_sr"].ToString(),
                            Name = dr["Name"].ToString(),
                            Doctor_name = dr["Doctor_name"].ToString(),
                            Billing_date = Convert.ToDateTime(dr["Billing_date"]),
                            Bill_Amount = Convert.ToDecimal(dr["Bill_Amount"]),
                            Discount = Convert.ToDecimal(dr["Discount"]),
                            Net_Payable = Convert.ToDecimal(dr["Net_Payable"]),
                            Received = Convert.ToDecimal(dr["Received"]),
                            Pending = Convert.ToDecimal(dr["Pending"]),
                            Hospital_Total = Convert.ToDecimal(dr["Hospital_Total"]),
                            Surgical_Total = Convert.ToDecimal(dr["Surgical_Total"]),
                            Medical_Record_Charges = Convert.ToDecimal(dr["Medical_Record_Charges"]),
                            BioMedical_Waste_Charges = Convert.ToDecimal(dr["BioMedical_Waste_Charges"]),
                            Administrative_Charges = Convert.ToDecimal(dr["Administrative_Charges"]),
                            Consultant_Visiting_Charges = Convert.ToDecimal(dr["Consultant_Visiting_Charges"]),
                            Nursing_Charges = Convert.ToDecimal(dr["Nursing_Charges"]),
                            Bed_Charges = Convert.ToDecimal(dr["Bed_Charges"]),
                            Lab_Total = Convert.ToDecimal(dr["Lab_Total"]),
                            Radiology_Total = Convert.ToDecimal(dr["Radiology_Total"]),
                            Registration_Charges = Convert.ToDecimal(dr["Registration_Charges"]),
                            Consultant_Charges = Convert.ToDecimal(dr["Consultant_Charges"])

                        });
                    }

                    return View(IPDBillinglistDate);
                    
                }

                else if(SearchValue != "All" && SearchBillPAyment != "All")
                {
                    //var Searchbyproductname = IPDBillinglistDate.Where(p => p.Branch.ToLower().Contains(SearchValue.ToLower()));
                    //return View(Searchbyproductname);
                    string Branch = SearchValue;
                    string PaymentMode = SearchBillPAyment;
                    string sqlQuery1 = @"SELECT        GID, Branch, PaymentMode, Patient_ID_sr, Name, Doctor_name, Billing_date, Bill_Amount, Discount, Net_Payable, Received, Pending, Hospital_Total, Surgical_Total, Medical_Record_Charges, BioMedical_Waste_Charges, 
                        Administrative_Charges, Consultant_Visiting_Charges, Nursing_Charges, Bed_Charges, Lab_Total, Radiology_Total, Registration_Charges, Consultant_Charges FROM  HMS_Global.IPD_Patient_Details where   Branch= '" + SearchValue + "'and PaymentMode ='" + PaymentMode + "' and Billing_date BETWEEN'" + fdate + "'and'" + tdate + "'";
                    SqlCommand cmd = new SqlCommand(sqlQuery1, con);
                    con.Open();
                    SqlDataAdapter sd1 = new SqlDataAdapter(cmd);
                    DataTable dt1 = new DataTable();
                    sd1.Fill(dt1);
                    con.Close();
                    decimal totalAmount = 0;
                    foreach (DataRow dr in dt1.Rows)
                    {
                        IPDBillinglistDate.Add(new IPDModel
                        {
                            GID = Convert.ToInt32(dr["GID"]),
                            Branch = dr["Branch"].ToString(),
                            PaymentMode = dr["PaymentMode"].ToString(),
                            Patient_ID_sr = dr["Patient_ID_sr"].ToString(),
                            Name = dr["Name"].ToString(),
                            Doctor_name = dr["Doctor_name"].ToString(),
                            Billing_date = Convert.ToDateTime(dr["Billing_date"]),
                            Bill_Amount = Convert.ToDecimal(dr["Bill_Amount"]),
                            Discount = Convert.ToDecimal(dr["Discount"]),
                            Net_Payable = Convert.ToDecimal(dr["Net_Payable"]),
                            Received = Convert.ToDecimal(dr["Received"]),
                            Pending = Convert.ToDecimal(dr["Pending"]),
                            Hospital_Total = Convert.ToDecimal(dr["Hospital_Total"]),
                            Surgical_Total = Convert.ToDecimal(dr["Surgical_Total"]),
                            Medical_Record_Charges = Convert.ToDecimal(dr["Medical_Record_Charges"]),
                            BioMedical_Waste_Charges = Convert.ToDecimal(dr["BioMedical_Waste_Charges"]),
                            Administrative_Charges = Convert.ToDecimal(dr["Administrative_Charges"]),
                            Consultant_Visiting_Charges = Convert.ToDecimal(dr["Consultant_Visiting_Charges"]),
                            Nursing_Charges = Convert.ToDecimal(dr["Nursing_Charges"]),
                            Bed_Charges = Convert.ToDecimal(dr["Bed_Charges"]),
                            Lab_Total = Convert.ToDecimal(dr["Lab_Total"]),
                            Radiology_Total = Convert.ToDecimal(dr["Radiology_Total"]),
                            Registration_Charges = Convert.ToDecimal(dr["Registration_Charges"]),
                            Consultant_Charges = Convert.ToDecimal(dr["Consultant_Charges"])


                        });
                    }
                }

                else if(SearchValue=="All" && SearchBillPAyment != "All")
                {
                    string PaymentMode = SearchBillPAyment;
                    string sqlQuery1 = @"SELECT        GID, Branch, PaymentMode, Patient_ID_sr, Name, Doctor_name, Billing_date, Bill_Amount, Discount, Net_Payable, Received, Pending, Hospital_Total, Surgical_Total, Medical_Record_Charges, BioMedical_Waste_Charges, 
                            Administrative_Charges, Consultant_Visiting_Charges, Nursing_Charges, Bed_Charges, Lab_Total, Radiology_Total, Registration_Charges, Consultant_Charges FROM  HMS_Global.IPD_Patient_Details where PaymentMode ='" + PaymentMode + "' and Billing_date BETWEEN'" + fdate + "'and'" + tdate + "'";
                    SqlCommand cmd = new SqlCommand(sqlQuery1, con);
                    con.Open();
                    SqlDataAdapter sd1 = new SqlDataAdapter(cmd);
                    DataTable dt1 = new DataTable();
                    sd1.Fill(dt1);
                    con.Close();
                    decimal totalAmount = 0;
                    foreach (DataRow dr in dt1.Rows)
                    {
                        IPDBillinglistDate.Add(new IPDModel
                        {
                            GID = Convert.ToInt32(dr["GID"]),
                            Branch = dr["Branch"].ToString(),
                            PaymentMode = dr["PaymentMode"].ToString(),
                            Patient_ID_sr = dr["Patient_ID_sr"].ToString(),
                            Name = dr["Name"].ToString(),
                            Doctor_name = dr["Doctor_name"].ToString(),
                            Billing_date = Convert.ToDateTime(dr["Billing_date"]),
                            Bill_Amount = Convert.ToDecimal(dr["Bill_Amount"]),
                            Discount = Convert.ToDecimal(dr["Discount"]),
                            Net_Payable = Convert.ToDecimal(dr["Net_Payable"]),
                            Received = Convert.ToDecimal(dr["Received"]),
                            Pending = Convert.ToDecimal(dr["Pending"]),
                            Hospital_Total = Convert.ToDecimal(dr["Hospital_Total"]),
                            Surgical_Total = Convert.ToDecimal(dr["Surgical_Total"]),
                            Medical_Record_Charges = Convert.ToDecimal(dr["Medical_Record_Charges"]),
                            BioMedical_Waste_Charges = Convert.ToDecimal(dr["BioMedical_Waste_Charges"]),
                            Administrative_Charges = Convert.ToDecimal(dr["Administrative_Charges"]),
                            Consultant_Visiting_Charges = Convert.ToDecimal(dr["Consultant_Visiting_Charges"]),
                            Nursing_Charges = Convert.ToDecimal(dr["Nursing_Charges"]),
                            Bed_Charges = Convert.ToDecimal(dr["Bed_Charges"]),
                            Lab_Total = Convert.ToDecimal(dr["Lab_Total"]),
                            Radiology_Total = Convert.ToDecimal(dr["Radiology_Total"]),
                            Registration_Charges = Convert.ToDecimal(dr["Registration_Charges"]),
                            Consultant_Charges = Convert.ToDecimal(dr["Consultant_Charges"])


                        });
                    }
                }

                else if (SearchValue != "All" && SearchBillPAyment == "All")
                {
                    string PaymentMode = SearchBillPAyment;
                    string sqlQuery1 = @"SELECT        GID, Branch, PaymentMode, Patient_ID_sr, Name, Doctor_name, Billing_date, Bill_Amount, Discount, Net_Payable, Received, Pending, Hospital_Total, Surgical_Total, Medical_Record_Charges, BioMedical_Waste_Charges, 
                            Administrative_Charges, Consultant_Visiting_Charges, Nursing_Charges, Bed_Charges, Lab_Total, Radiology_Total, Registration_Charges, Consultant_Charges FROM  HMS_Global.IPD_Patient_Details where Branch= '" + SearchValue + "'and Billing_date BETWEEN'" + fdate + "'and'" + tdate + "'";
                    SqlCommand cmd = new SqlCommand(sqlQuery1, con);
                    con.Open();
                    SqlDataAdapter sd1 = new SqlDataAdapter(cmd);
                    DataTable dt1 = new DataTable();
                    sd1.Fill(dt1);
                    con.Close();
                    decimal totalAmount = 0;
                    foreach (DataRow dr in dt1.Rows)
                    {
                        IPDBillinglistDate.Add(new IPDModel
                        {
                            GID = Convert.ToInt32(dr["GID"]),
                            Branch = dr["Branch"].ToString(),
                            PaymentMode = dr["PaymentMode"].ToString(),
                            Patient_ID_sr = dr["Patient_ID_sr"].ToString(),
                            Name = dr["Name"].ToString(),
                            Doctor_name = dr["Doctor_name"].ToString(),
                            Billing_date = Convert.ToDateTime(dr["Billing_date"]),
                            Bill_Amount = Convert.ToDecimal(dr["Bill_Amount"]),
                            Discount = Convert.ToDecimal(dr["Discount"]),
                            Net_Payable = Convert.ToDecimal(dr["Net_Payable"]),
                            Received = Convert.ToDecimal(dr["Received"]),
                            Pending = Convert.ToDecimal(dr["Pending"]),
                            Hospital_Total = Convert.ToDecimal(dr["Hospital_Total"]),
                            Surgical_Total = Convert.ToDecimal(dr["Surgical_Total"]),
                            Medical_Record_Charges = Convert.ToDecimal(dr["Medical_Record_Charges"]),
                            BioMedical_Waste_Charges = Convert.ToDecimal(dr["BioMedical_Waste_Charges"]),
                            Administrative_Charges = Convert.ToDecimal(dr["Administrative_Charges"]),
                            Consultant_Visiting_Charges = Convert.ToDecimal(dr["Consultant_Visiting_Charges"]),
                            Nursing_Charges = Convert.ToDecimal(dr["Nursing_Charges"]),
                            Bed_Charges = Convert.ToDecimal(dr["Bed_Charges"]),
                            Lab_Total = Convert.ToDecimal(dr["Lab_Total"]),
                            Radiology_Total = Convert.ToDecimal(dr["Radiology_Total"]),
                            Registration_Charges = Convert.ToDecimal(dr["Registration_Charges"]),
                            Consultant_Charges = Convert.ToDecimal(dr["Consultant_Charges"])


                        });
                    }
                }
                return View(IPDBillinglistDate);
            }
            else
            {
                //ViewBag.ErrorMsg = "Please select dates!!".ToString();
               return View(IPDBillinglist);
            }

           // return View(IPDBillinglist);
        }
        public ActionResult IPDDischargesIndex(string searchBy, string SearchValue, DateTime? Fromdate, DateTime? Todate)
        {
            var IPDDischargeslist = IPDDB.GetIPDDischarges();

            if (Fromdate != null && Todate != null)
            {
                //string formattedDate = Convert.ToString(Fromdate);
                //DateTime originalDateTime = DateTime.ParseExact(formattedDate.Trim('{', '}'), "dd-MM-yyyy hh:mm:ss tt", System.Globalization.CultureInfo.InvariantCulture);
                string fdate = Fromdate.Value.ToString("yyyy-MM-dd");

                //string formattedToDate = Convert.ToString(Todate);
               // DateTime originalToDateTime = DateTime.ParseExact(formattedToDate.Trim('{', '}'), "dd-MM-yyyy hh:mm:ss tt", System.Globalization.CultureInfo.InvariantCulture);
                string tdate = Todate.Value.ToString("yyyy-MM-dd");

                connection();
                List<IPDModel> IPDDischargeslistDate = new List<IPDModel>();
                string sqlQuery = @"SELECT Branch, Patient_ID_sr, Name, Doctor_name,Admission_Date, Discharge_Date, Bill_Amount, Discount, Received, Pending, Registration_Charges, Consultant_Charges, Nursing_Charges, Bed_Charges, Hospital_Total, Surgical_Total, Medical_Record_Charges, BioMedical_Waste_Charges, Consultant_Visiting_Charges, Administrative_Charges FROM HMS_Global.IPD_Patient_Details WHERE Discharge_Date is not null and Discharge_Date BETWEEN '" + fdate + "'and'" + tdate + "'";
                SqlCommand command = new SqlCommand(sqlQuery, con);
                con.Open();
                SqlDataAdapter sd = new SqlDataAdapter(command);
                DataTable dt = new DataTable();
                sd.Fill(dt);

                con.Close();
                foreach (DataRow dr in dt.Rows)
                {
                    IPDDischargeslistDate.Add(new IPDModel
                    {
                        Branch = dr["Branch"].ToString(),
                        Patient_ID_sr = dr["Patient_ID_sr"].ToString(),
                        Name = dr["Name"].ToString(),
                        Doctor_name = dr["Doctor_name"].ToString(),
                        Admission_Date = Convert.ToDateTime(dr["Admission_Date"]),
                        Discharge_Date = Convert.ToDateTime(dr["Discharge_Date"]),
                        Discount = Convert.ToDecimal(dr["Discount"]),
                        Bill_Amount = Convert.ToDecimal(dr["Bill_Amount"]),
                        Received = Convert.ToDecimal(dr["Received"]),
                        Pending = Convert.ToDecimal(dr["Pending"]),
                        Registration_Charges = Convert.ToDecimal(dr["Registration_Charges"]),
                        Consultant_Charges = Convert.ToDecimal(dr["Consultant_Charges"]),
                        Nursing_Charges = Convert.ToDecimal(dr["Nursing_Charges"]),
                        Bed_Charges = Convert.ToDecimal(dr["Bed_Charges"]),
                        Hospital_Total = Convert.ToDecimal(dr["Hospital_Total"]),
                        Surgical_Total = Convert.ToDecimal(dr["Surgical_Total"]),
                        Medical_Record_Charges = Convert.ToDecimal(dr["Medical_Record_Charges"]),
                        BioMedical_Waste_Charges = Convert.ToDecimal(dr["BioMedical_Waste_Charges"]),
                        Consultant_Visiting_Charges = Convert.ToDecimal(dr["Consultant_Visiting_Charges"]),
                        Administrative_Charges = Convert.ToDecimal(dr["Administrative_Charges"])

                    });
                }
                if (SearchValue == "All")
                {
                    return View(IPDDischargeslistDate);
                }
                else
                {
                    var Searchbyproductname = IPDDischargeslistDate.Where(p => p.Branch.ToLower().Contains(SearchValue.ToLower()));
                    return View(Searchbyproductname);
                }

            }
            else
            {
                return View(IPDDischargeslist);
            }
            return View(IPDDischargeslist);
        }
        public ActionResult ExportToTallyIndex(string searchBy, string SearchValue, DateTime? Fromdate, DateTime? Todate)
        {
            var IPDBillinglist = IPDDB.GetIPDBillings();
            if (Fromdate != null && Todate != null)
            {
                //string formattedDate = Convert.ToString(Fromdate);
               // DateTime originalDateTime = DateTime.ParseExact(formattedDate.Trim('{', '}'), "dd-MM-yyyy hh:mm:ss tt", System.Globalization.CultureInfo.InvariantCulture);
                string fdate = Fromdate.Value.ToString("yyyy-MM-dd");

                //string formattedToDate = Convert.ToString(Todate);
               // DateTime originalToDateTime = DateTime.ParseExact(formattedToDate.Trim('{', '}'), "dd-MM-yyyy hh:mm:ss tt", System.Globalization.CultureInfo.InvariantCulture);
                string tdate = Todate.Value.ToString("yyyy-MM-dd");

                connection();
                List<IPDModel> IPDBillinglistDateList = new List<IPDModel>();
                string sqlQuery = @"SELECT GID, Branch, PatientID_OPD_IPD, Date, Patient_Name, Purpose, Total_Amount, Discount, Advancce, Net_Payable, Received, Pending, Test_Charges, Bed_Charges, Surgical_Proc_Charges, Procedure_Charges,  Consultant_Charges, Other_Charges FROM HMS_Global.All_AccountingDetails WHERE Date BETWEEN   '" + fdate + "'and'" + tdate + "'";
                SqlCommand command = new SqlCommand(sqlQuery, con);
                con.Open();
                SqlDataAdapter sd = new SqlDataAdapter(command);
                DataTable dt = new DataTable();
                sd.Fill(dt);
                con.Close();
                foreach (DataRow dr in dt.Rows)
                {
                    IPDBillinglistDateList.Add(new IPDModel
                    {
                        GID = Convert.ToInt32(dr["GID"]),
                        Branch = dr["Branch"].ToString(),
                        PatientID_OPD_IPD = dr["PatientID_OPD_IPD"].ToString(),
                        Date = Convert.ToDateTime(dr["Date"]),
                        Patient_Name = dr["Patient_Name"].ToString(),
                        Purpose = dr["Purpose"].ToString(),
                        Total_Amount = Convert.ToDecimal(dr["Total_Amount"]),
                        Discount = Convert.ToDecimal(dr["Discount"]),
                        Advancce = Convert.ToDecimal(dr["Advancce"]),
                        Net_Payable = Convert.ToDecimal(dr["Net_Payable"]),
                        Received = Convert.ToDecimal(dr["Received"]),
                        Pending = Convert.ToDecimal(dr["Pending"]),
                        Test_Charges = Convert.ToDecimal(dr["Test_Charges"]),
                        Bed_Charges = Convert.ToDecimal(dr["Bed_Charges"]),
                        Surgical_Proc_Charges = Convert.ToDecimal(dr["Surgical_Proc_Charges"]),
                        Procedure_Charges = Convert.ToDecimal(dr["Procedure_Charges"]),
                        Consultant_Charges = Convert.ToDecimal(dr["Consultant_Charges"]),
                        Other_Charges = Convert.ToDecimal(dr["Other_Charges"]),

                    });
                }
                if (SearchValue == "All")
                {
                    return View(IPDBillinglistDateList);
                }
                else
                {
                    var Searchbyproductname = IPDBillinglistDateList.Where(p => p.Branch.ToLower().Contains(SearchValue.ToLower()));
                    return View(Searchbyproductname);
                }

            }
            else
            {
                return View(IPDBillinglist);
            }
            return View(IPDBillinglist);
        }
        public ActionResult ExportToExcel()
        {

            var gv = new GridView();
            gv.DataSource = this.IPDDB.GetExportBillings();
            //gv.DataSource = this.IPDBillinglistDateList;
            gv.DataBind();
            Response.ClearContent();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment; filename=PatientDetailBilling.xls");
            Response.ContentType = "application/ms-excel";
            Response.Charset = "";
            StringWriter objStringWriter = new StringWriter();
            HtmlTextWriter objHtmlTextWriter = new HtmlTextWriter(objStringWriter);
            gv.RenderControl(objHtmlTextWriter);
            Response.Output.Write(objStringWriter.ToString());
            Response.Flush();
            Response.End();
            ViewBag.Message = "Downloaded File Successfully";
            return View("ExportToExcel");
        }
        // GET: IPD/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: IPD/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: IPD/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: IPD/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: IPD/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: IPD/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: IPD/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
