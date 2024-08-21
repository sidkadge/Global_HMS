using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class HMS_GlobalController : Controller
    {
        private SqlConnection con;
        string conString = ConfigurationManager.ConnectionStrings["Connection"].ToString();
        //string conString = ConfigurationSettings.AppSettings("Connection");
        private void connection()
        {
            con = new SqlConnection(conString);
        }
        OPDDBHandler OPDDB = new OPDDBHandler();
        // GET: Default
        public ActionResult Index()
        {
           var opdDetails= OPDDB.ShowAllOPdData();
           var IPDPatientDetailsFetching = OPDDB.ShowAllIPDData();
           var OnlyTestPatientDetailsFetching = OPDDB.ShowAllOnlyData();
            return View();
        }
         
        public ActionResult IndexOPD(string searchBy ,string SearchValue, DateTime? Fromdate, DateTime? Todate)
        {
            var OPDBillinglist = OPDDB.GetOPDBillings();
          
           

                if (Fromdate != null && Todate != null)
                {


                    //string formattedDate = Convert.ToString(Fromdate);
                    //DateTime originalDateTime = DateTime.ParseExact(formattedDate.Trim('{', '}'), "dd-MM-yyyy hh:mm:ss tt", System.Globalization.CultureInfo.InvariantCulture);
                    string fdate = Fromdate.Value.ToString("yyyy-MM-dd");

                    //string formattedToDate = Convert.ToString(Todate);
                   // DateTime originalToDateTime = DateTime.ParseExact(formattedToDate.Trim('{', '}'), "dd-MM-yyyy hh:mm:ss tt", System.Globalization.CultureInfo.InvariantCulture);
                    string tdate = Todate.Value.ToString("yyyy-MM-dd");


                    connection();

                    List<OPDBillingViewModel> OPDBillinglistDAte = new List<OPDBillingViewModel>();

                    string sqlQuery = @"SELECT        HMS_Global.OPD_Patient_Details.Branch, HMS_Global.Patient_Details.Patient_ID, HMS_Global.OPD_Patient_Details.OPDID_Sr, HMS_Global.Patient_Details.Patient_Name, HMS_Global.Patient_Details.Doctor_Name, 
                         HMS_Global.OPD_Patient_Details.Date, HMS_Global.OPD_Patient_Details.Bill_Amount, HMS_Global.OPD_Patient_Details.Received, HMS_Global.OPD_Patient_Details.Pending, HMS_Global.OPD_Patient_Details.Lab_Total, 
                         HMS_Global.OPD_Patient_Details.Radiology_Total, HMS_Global.OPD_Patient_Details.Procedure_Total, HMS_Global.OPD_Patient_Details.Consultant_Charges, HMS_Global.OPD_Patient_Details.Registration_Charges, 
                         HMS_Global.Patient_Details.Branch AS Expr1
FROM            HMS_Global.Patient_Details INNER JOIN
                         HMS_Global.OPD_Patient_Details ON HMS_Global.Patient_Details.PID = HMS_Global.OPD_Patient_Details.PID AND HMS_Global.Patient_Details.Branch = HMS_Global.OPD_Patient_Details.Branch
                WHERE HMS_Global.OPD_Patient_Details.Date BETWEEN '" + fdate + "'and'" + tdate + "'";
                    SqlCommand command = new SqlCommand(sqlQuery, con);
                    con.Open();
                    SqlDataAdapter sd = new SqlDataAdapter(command);
                    DataTable dt = new DataTable();
                    sd.Fill(dt);
                    con.Close();
                    foreach (DataRow dr in dt.Rows)
                    {
                        OPDBillinglistDAte.Add(new OPDBillingViewModel
                        {

                            Branch = dr["Branch"].ToString(),
                            Patient_ID = dr["Patient_ID"].ToString(),
                            OPDID_Sr = dr["OPDID_Sr"].ToString(),
                            Patient_Name = dr["Patient_Name"].ToString(),
                            Doctor_Name = dr["Doctor_Name"].ToString(),
                            Date = Convert.ToDateTime(dr["Date"]),
                            Bill_Amount = Convert.ToDecimal(dr["Bill_Amount"]),
                            Received = Convert.ToDecimal(dr["Received"]),
                            Pending = Convert.ToDecimal(dr["Pending"]),
                            Lab_Total = Convert.ToDecimal(dr["Lab_Total"]),
                            Radiology_Total = Convert.ToDecimal(dr["Radiology_Total"]),
                            Procedure_Total = Convert.ToDecimal(dr["Procedure_Total"]),
                            Consultant_Charges = Convert.ToDecimal(dr["Consultant_Charges"]),
                            Registration_Charges = Convert.ToDecimal(dr["Registration_Charges"])

                        });
                    
                    }
                    if (SearchValue == "All")
                    {
                        return View(OPDBillinglistDAte);
                    }
                    else
                    {

                        var Searchbyproductname = OPDBillinglistDAte.Where(p => p.Branch.ToLower().Contains(SearchValue.ToLower()));
                        return View(Searchbyproductname);

                    }

                }
                else
                {
                    return View(OPDBillinglist);
                }

          
            return View(OPDBillinglist);
        }
       
        public ActionResult IndexOPDPatientDetails(string searchBy, string SearchValue, DateTime? Fromdate, DateTime? Todate)
        {
            var OPDPatientDetails = OPDDB.GetOPDPatientDetails();

            if (Fromdate != null && Todate != null)
            {

                //string formattedDate = Convert.ToString(Fromdate);
                //DateTime originalDateTime = DateTime.ParseExact(formattedDate.Trim('{', '}'), "dd-MM-yyyy hh:mm:ss tt", System.Globalization.CultureInfo.InvariantCulture);
                string fdate = Fromdate.Value.ToString("yyyy-MM-dd");

                //string formattedToDate = Convert.ToString(Todate);
               // DateTime originalToDateTime = DateTime.ParseExact(formattedToDate.Trim('{', '}'), "dd-MM-yyyy hh:mm:ss tt", System.Globalization.CultureInfo.InvariantCulture);
                string tdate = Todate.Value.ToString("yyyy-MM-dd");

                connection();


                List<OPDBillingViewModel> OPDPatientDetailsDAte = new List<OPDBillingViewModel>();

                string sqlQuery = @"SELECT        HMS_Global.OPD_Patient_Details.Branch, HMS_Global.Patient_Details.Patient_ID, HMS_Global.OPD_Patient_Details.OPDID_Sr, HMS_Global.Patient_Details.Patient_Name, HMS_Global.Patient_Details.Doctor_Name, 
                         HMS_Global.OPD_Patient_Details.Date, HMS_Global.Patient_Details.Branch AS Expr1
FROM            HMS_Global.Patient_Details INNER JOIN
                         HMS_Global.OPD_Patient_Details ON HMS_Global.Patient_Details.PID = HMS_Global.OPD_Patient_Details.PID AND HMS_Global.Patient_Details.Branch = HMS_Global.OPD_Patient_Details.Branch
                WHERE HMS_Global.OPD_Patient_Details.Date BETWEEN '" + fdate + "'and'" + tdate + "'";
                SqlCommand command = new SqlCommand(sqlQuery, con);
                con.Open();
                SqlDataAdapter sd = new SqlDataAdapter(command);
                DataTable dt = new DataTable();
                sd.Fill(dt);
                con.Close();
                foreach (DataRow dr in dt.Rows)
                {
                    OPDPatientDetailsDAte.Add(new OPDBillingViewModel
                    {

                        Branch = dr["Branch"].ToString(),
                        Patient_ID = dr["Patient_ID"].ToString(),
                        OPDID_Sr = dr["OPDID_Sr"].ToString(),
                        Patient_Name = dr["Patient_Name"].ToString(),
                        Doctor_Name = dr["Doctor_Name"].ToString(),
                        AdmissionDate = Convert.ToDateTime(dr["Date"])
                        

                    });
                }
                if (SearchValue == "All")
                {
                    return View(OPDPatientDetailsDAte);
                }

                else
                {
                    var Searchbyproductname = OPDPatientDetailsDAte.Where(p => p.Branch.ToLower().Contains(SearchValue.ToLower()));
                    return View(Searchbyproductname);

                }

            }
            else
            {
                return View(OPDPatientDetails);
            }
            return View(OPDPatientDetails);

        }
        public ActionResult IndexAdd()
        {
            return View();
        }
        public ActionResult IndexSearch(string SearchValue)
        {
            //var OPDBillinglist = OPDDB.GetOPDBillingsSearch();
            return View();
           
        }
    }
}