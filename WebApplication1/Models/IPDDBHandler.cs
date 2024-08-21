using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class IPDDBHandler
    {
        private SqlConnection con;
        string conString = ConfigurationManager.ConnectionStrings["Connection"].ToString();
        //string conString = ConfigurationSettings.AppSettings("Connection");
        private void connection()
        {
            con = new SqlConnection(conString);
        }
        public List<IPDModel> GetIPDPatientDetails()
        {
            connection();
            List<IPDModel> IPDPatientDetails = new List<IPDModel>();

            SqlCommand command = new SqlCommand("sp_IPDPatientDetails", con);
            command.CommandType = CommandType.StoredProcedure;

            SqlDataAdapter sd = new SqlDataAdapter(command);
            DataTable dt = new DataTable();
            con.Open();
            sd.Fill(dt);
            con.Close();
            foreach (DataRow dr in dt.Rows)
            {
                IPDPatientDetails.Add(new IPDModel
                {            
                    Branch = dr["Branch"].ToString(),
                    Patient_ID_sr = dr["Patient_ID_sr"].ToString(),
                    IPD_ID_Sr = dr["IPD_ID_Sr"].ToString(),
                    Name = dr["Name"].ToString(),
                    Doctor_name = dr["Doctor_name"].ToString(),
                    Admission_Date = Convert.ToDateTime(dr["Admission_Date"]),
                    Discharge_Date = dr["Discharge_Date"] != DBNull.Value ? (DateTime?)Convert.ToDateTime(dr["Discharge_Date"]) : (DateTime?)null,
                });
            }

            return IPDPatientDetails;


        }
        public List<IPDModel> GetIPDBillings()
        {
            connection();
            List<IPDModel> IPDBillinglist = new List<IPDModel>();

            SqlCommand command = new SqlCommand("sp_IPDBilling", con);
            command.CommandType = CommandType.StoredProcedure;

            SqlDataAdapter sd = new SqlDataAdapter(command);
            DataTable dt = new DataTable();
            con.Open();
            sd.Fill(dt);
            con.Close();
            if (dt.Rows.Count == 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    IPDBillinglist.Add(new IPDModel
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
            }
            return IPDBillinglist;


        }
        public List<IPDModel> GetIPDDischarges()
        {
            connection();
            List<IPDModel> IPDDischargeslist = new List<IPDModel>();

            SqlCommand command = new SqlCommand("sp_IPDischarges", con);
            command.CommandType = CommandType.StoredProcedure;

            SqlDataAdapter sd = new SqlDataAdapter(command);
            DataTable dt = new DataTable();
            con.Open();
            sd.Fill(dt);
            con.Close();
            foreach (DataRow dr in dt.Rows)
            {
                IPDDischargeslist.Add(new IPDModel
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

            return IPDDischargeslist;


        }

        public List<ExportToExcelModel> GetExportBillings()
        {
            connection();
            List<ExportToExcelModel> IPDBillinglist = new List<ExportToExcelModel>();

            SqlCommand command = new SqlCommand("sp_IPDBilling", con);
            command.CommandType = CommandType.StoredProcedure;

            SqlDataAdapter sd = new SqlDataAdapter(command);
            DataTable dt = new DataTable();
            con.Open();
            sd.Fill(dt);
            con.Close();
            foreach (DataRow dr in dt.Rows)
            {
                IPDBillinglist.Add(new ExportToExcelModel
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

            return IPDBillinglist;


        }



    }
}