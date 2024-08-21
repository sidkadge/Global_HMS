using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class DiagnosticController : Controller
    {           
        private SqlConnection con;
        string conString = ConfigurationManager.ConnectionStrings["Connection"].ToString();
        //string conString = ConfigurationSettings.AppSettings("Connection");
        private void connection()
        {
            con = new SqlConnection(conString);
        }
        TestDBHandler testDB = new TestDBHandler();
        // GET: Test
        public ActionResult IndexTest(string searchBy, string SearchValue, string SearchValueLR, DateTime? Fromdate, DateTime? Todate)
        {
            var OnlyTestlist = testDB.GetOnlyTest();


            if (Fromdate != null && Todate != null)
            {


               // string formattedDate = Convert.ToString(Fromdate);
                //DateTime originalDateTime = DateTime.ParseExact(formattedDate.Trim('{', '}'), "dd-MM-yyyy hh:mm:ss tt", System.Globalization.CultureInfo.InvariantCulture);
                string fdate = Fromdate.Value.ToString("yyyy-MM-dd");

                //string formattedToDate = Convert.ToString(Todate);
                //DateTime originalToDateTime = DateTime.ParseExact(formattedToDate.Trim('{', '}'), "dd-MM-yyyy hh:mm:ss tt", System.Globalization.CultureInfo.InvariantCulture);
                string tdate = Todate.Value.ToString("yyyy-MM-dd");


                connection();

                List<OnlyTestModel> OnlyTestlistDate = new List<OnlyTestModel>();

                if (SearchValueLR == "All")
                {
                    string sqlQuery2 = @"SELECT        HMS_Global.OnlyTest_PatientDetails.Branch, HMS_Global.OnlyTest_PatientDetails.OPDID_Sr, HMS_Global.OnlyTest_PatientDetails.Patient_Name, HMS_Global.Patient_Details.Doctor_Name, 
                         HMS_Global.OnlyTest_PatientDetails.Date, HMS_Global.OnlyTest_PatientDetails.Bill_Amount, HMS_Global.OnlyTest_PatientDetails.Received, HMS_Global.OnlyTest_PatientDetails.Pending, 
                         HMS_Global.OnlyTest_PatientDetails.Lab_Total, HMS_Global.OnlyTest_PatientDetails.Radiology_Total, HMS_Global.OnlyTest_PatientDetails.Consultant_Charges, 
                         HMS_Global.OnlyTest_PatientDetails.Registration_Charges
FROM            HMS_Global.OnlyTest_PatientDetails INNER JOIN
                         HMS_Global.Patient_Details ON HMS_Global.OnlyTest_PatientDetails.PID = HMS_Global.Patient_Details.PID AND HMS_Global.OnlyTest_PatientDetails.Branch = HMS_Global.Patient_Details.Branch WHERE HMS_Global.OnlyTest_PatientDetails.Date BETWEEN '" + fdate + "'and'" + tdate + "'";
                    SqlCommand command = new SqlCommand(sqlQuery2, con);
                    con.Open();
                    SqlDataAdapter sd = new SqlDataAdapter(command);
                    DataTable dt = new DataTable();
                    sd.Fill(dt);
                    con.Close();
                    foreach (DataRow dr in dt.Rows)
                    {
                        OnlyTestlistDate.Add(new OnlyTestModel
                        {
                            Branch = dr["Branch"].ToString(),
                            OPDID_Sr = dr["OPDID_Sr"].ToString(),
                            Patient_Name = dr["Patient_Name"].ToString(),
                            Doctor_Name = dr["Doctor_Name"].ToString(),
                            Date = Convert.ToDateTime(dr["Date"]),
                            Bill_Amount = Convert.ToDecimal(dr["Bill_Amount"]),
                            Received = Convert.ToDecimal(dr["Received"]),
                            Pending = Convert.ToDecimal(dr["Pending"]),
                            Lab_Total = Convert.ToDecimal(dr["Lab_Total"]),
                            Radiology_Total = Convert.ToDecimal(dr["Radiology_Total"]),
                            Consultant_Charges = Convert.ToDecimal(dr["Consultant_Charges"]),
                            Registration_Charges = Convert.ToDecimal(dr["Registration_Charges"])


                        });
                    }
                }
                if (SearchValueLR == "Lab")
                {
                    string labvalue = SearchValueLR;
                    string sqlQuery1 = @"SELECT        HMS_Global.OnlyTest_PatientDetails.Branch, HMS_Global.OnlyTest_PatientDetails.OPDID_Sr, HMS_Global.OnlyTest_PatientDetails.Patient_Name, HMS_Global.Patient_Details.Doctor_Name, 
                         HMS_Global.OnlyTest_PatientDetails.Date, HMS_Global.OnlyTest_PatientDetails.Bill_Amount, HMS_Global.OnlyTest_PatientDetails.Received, HMS_Global.OnlyTest_PatientDetails.Pending, 
                         HMS_Global.OnlyTest_PatientDetails.Lab_Total, HMS_Global.OnlyTest_PatientDetails.Radiology_Total, HMS_Global.OnlyTest_PatientDetails.Consultant_Charges, 
                         HMS_Global.OnlyTest_PatientDetails.Registration_Charges
FROM            HMS_Global.OnlyTest_PatientDetails INNER JOIN
                         HMS_Global.Patient_Details ON HMS_Global.OnlyTest_PatientDetails.PID = HMS_Global.Patient_Details.PID AND HMS_Global.OnlyTest_PatientDetails.Branch = HMS_Global.Patient_Details.Branch WHERE HMS_Global.OnlyTest_PatientDetails.Lab_Total > '0.00' and HMS_Global.OnlyTest_PatientDetails.Date BETWEEN '" + fdate + "'and'" + tdate + "' ";
                    SqlCommand cmd = new SqlCommand(sqlQuery1, con);
                    con.Open();
                    SqlDataAdapter sd1 = new SqlDataAdapter(cmd);
                    DataTable dt1 = new DataTable();
                    sd1.Fill(dt1);
                    con.Close();
                    foreach (DataRow dr in dt1.Rows)
                    {
                        OnlyTestlistDate.Add(new OnlyTestModel
                        {
                            Branch = dr["Branch"].ToString(),
                            OPDID_Sr = dr["OPDID_Sr"].ToString(),
                            Patient_Name = dr["Patient_Name"].ToString(),
                            Doctor_Name = dr["Doctor_Name"].ToString(),
                            Date = Convert.ToDateTime(dr["Date"]),
                            Bill_Amount = Convert.ToDecimal(dr["Bill_Amount"]),
                            Received = Convert.ToDecimal(dr["Received"]),
                            Pending = Convert.ToDecimal(dr["Pending"]),
                            Lab_Total = Convert.ToDecimal(dr["Lab_Total"]),
                            Radiology_Total = Convert.ToDecimal(dr["Radiology_Total"]),
                            Consultant_Charges = Convert.ToDecimal(dr["Consultant_Charges"]),
                            Registration_Charges = Convert.ToDecimal(dr["Registration_Charges"])

                        });
                    }

                }
                if (SearchValueLR == "Radiology")
                {
                    string labvalue = SearchValueLR;
                    string sqlQuery1 = @"SELECT        HMS_Global.OnlyTest_PatientDetails.Branch, HMS_Global.OnlyTest_PatientDetails.OPDID_Sr, HMS_Global.OnlyTest_PatientDetails.Patient_Name, HMS_Global.Patient_Details.Doctor_Name, 
                         HMS_Global.OnlyTest_PatientDetails.Date, HMS_Global.OnlyTest_PatientDetails.Bill_Amount, HMS_Global.OnlyTest_PatientDetails.Received, HMS_Global.OnlyTest_PatientDetails.Pending, 
                         HMS_Global.OnlyTest_PatientDetails.Lab_Total, HMS_Global.OnlyTest_PatientDetails.Radiology_Total, HMS_Global.OnlyTest_PatientDetails.Consultant_Charges, 
                         HMS_Global.OnlyTest_PatientDetails.Registration_Charges
FROM            HMS_Global.OnlyTest_PatientDetails INNER JOIN
                         HMS_Global.Patient_Details ON HMS_Global.OnlyTest_PatientDetails.PID = HMS_Global.Patient_Details.PID AND HMS_Global.OnlyTest_PatientDetails.Branch = HMS_Global.Patient_Details.Branch WHERE HMS_Global.OnlyTest_PatientDetails.Radiology_Total >0.00 and HMS_Global.OnlyTest_PatientDetails.Date BETWEEN '" + fdate + "'and'" + tdate + "'";
                    SqlCommand cmd = new SqlCommand(sqlQuery1, con);
                    con.Open();
                    SqlDataAdapter sd1 = new SqlDataAdapter(cmd);
                    DataTable dt1 = new DataTable();
                    sd1.Fill(dt1);
                    con.Close();
                    foreach (DataRow dr in dt1.Rows)
                    {
                        OnlyTestlistDate.Add(new OnlyTestModel
                        {
                            Branch = dr["Branch"].ToString(),
                            OPDID_Sr = dr["OPDID_Sr"].ToString(),
                            Patient_Name = dr["Patient_Name"].ToString(),
                            Doctor_Name = dr["Doctor_Name"].ToString(),
                            Date = Convert.ToDateTime(dr["Date"]),
                            Bill_Amount = Convert.ToDecimal(dr["Bill_Amount"]),
                            Received = Convert.ToDecimal(dr["Received"]),
                            Pending = Convert.ToDecimal(dr["Pending"]),
                            Lab_Total = Convert.ToDecimal(dr["Lab_Total"]),
                            Radiology_Total = Convert.ToDecimal(dr["Radiology_Total"]),
                            Consultant_Charges = Convert.ToDecimal(dr["Consultant_Charges"]),
                            Registration_Charges = Convert.ToDecimal(dr["Registration_Charges"])


                        });
                    }

                }
                if (SearchValue == "All")
                {
                    return View(OnlyTestlistDate);
                }
                else
                {

                    var Searchbyproductname = OnlyTestlistDate.Where(p => p.Branch.ToLower().Contains(SearchValue.ToLower()));
                    return View(Searchbyproductname);

                }


                return View(OnlyTestlist);

            }
            else
            {
                return View(OnlyTestlist);
               
            }
           // return View(OnlyTestlist);


        }
        public ActionResult IndexTestAll(string searchBy, string SearchValue, string SearchValueLR, DateTime? Fromdate, DateTime? Todate)
        {
            var OnlyTestlist = testDB.GetOnlyTest();
            if (Fromdate != null && Todate != null)
            {


               // string formattedDate = Convert.ToString(Fromdate);
               // DateTime originalDateTime = DateTime.ParseExact(formattedDate.Trim('{', '}'), "dd-MM-yyyy hh:mm:ss tt", System.Globalization.CultureInfo.InvariantCulture);
                string fdate = Fromdate.Value.ToString("yyyy-MM-dd");

                //string formattedToDate = Convert.ToString(Todate);
               // DateTime originalToDateTime = DateTime.ParseExact(formattedToDate.Trim('{', '}'), "dd-MM-yyyy hh:mm:ss tt", System.Globalization.CultureInfo.InvariantCulture);
                string tdate = Todate.Value.ToString("yyyy-MM-dd");


                connection();

                List<OnlyTestModel> OnlyTestlistDate = new List<OnlyTestModel>();

                
                    string sqlQuery2 = @"SELECT       HMS_Global.OnlyTest_PatientDetails.Branch, HMS_Global.OnlyTest_PatientDetails.OPDID_Sr, HMS_Global.OnlyTest_PatientDetails.Patient_Name, HMS_Global.Patient_Details.Doctor_Name, HMS_Global.OnlyTest_PatientDetails.Date, 
                         HMS_Global.OnlyTest_PatientDetails.Bill_Amount, HMS_Global.OnlyTest_PatientDetails.Received, HMS_Global.OnlyTest_PatientDetails.Pending, HMS_Global.OnlyTest_PatientDetails.Lab_Total, 
                         HMS_Global.OnlyTest_PatientDetails.Radiology_Total, HMS_Global.OnlyTest_PatientDetails.Consultant_Charges, HMS_Global.OnlyTest_PatientDetails.Registration_Charges
FROM            HMS_Global.OnlyTest_PatientDetails INNER JOIN
                         HMS_Global.Patient_Details ON HMS_Global.OnlyTest_PatientDetails.PID = HMS_Global.Patient_Details.PID AND HMS_Global.OnlyTest_PatientDetails.Branch = HMS_Global.Patient_Details.Branch WHERE HMS_Global.OnlyTest_PatientDetails.Date BETWEEN '" + fdate + "'and'" + tdate + "'";
                    SqlCommand command = new SqlCommand(sqlQuery2, con);
                    con.Open();
                    SqlDataAdapter sd = new SqlDataAdapter(command);
                    DataTable dt = new DataTable();
                    sd.Fill(dt);
                    con.Close();
                    foreach (DataRow dr in dt.Rows)
                    {
                        OnlyTestlistDate.Add(new OnlyTestModel
                        {
                            Branch = dr["Branch"].ToString(),
                            OPDID_Sr = dr["OPDID_Sr"].ToString(),
                            Patient_Name = dr["Patient_Name"].ToString(),
                            Doctor_Name = dr["Doctor_Name"].ToString(),
                            Date = Convert.ToDateTime(dr["Date"]),
                            Bill_Amount = Convert.ToDecimal(dr["Bill_Amount"]),
                            Received = Convert.ToDecimal(dr["Received"]),
                            Pending = Convert.ToDecimal(dr["Pending"]),
                            Lab_Total = Convert.ToDecimal(dr["Lab_Total"]),
                            Radiology_Total = Convert.ToDecimal(dr["Radiology_Total"]),
                            Consultant_Charges = Convert.ToDecimal(dr["Consultant_Charges"]),
                            Registration_Charges = Convert.ToDecimal(dr["Registration_Charges"])


                        });
                    }
                
                
                if (SearchValue == "All")
                {
                    return View(OnlyTestlistDate);
                }
                else
                {

                    var Searchbyproductname = OnlyTestlistDate.Where(p => p.Branch.ToLower().Contains(SearchValue.ToLower()));
                    return View(Searchbyproductname);

                }


              //return View(OnlyTestlist);

            }
           //// else
            //{
              return View(OnlyTestlist);

           // }

        }

            // GET: Test/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Test/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Test/Create
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

        // GET: Test/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Test/Edit/5
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

        // GET: Test/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Test/Delete/5
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
