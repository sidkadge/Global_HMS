using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;


namespace WebApplication1.Models
{
    public class OPDDBHandler
    {
        public decimal Procedure_Total;
        public int OPDID;
        public DateTime Date;
        public string Purpose;
        public Decimal Lab_Total;
        public decimal Radiology_Total;
        public decimal Bill_Amount;
        public decimal Received;
        public decimal Pending;
        public string Patient_Name;
        public decimal Consultant_Charges;
        public decimal Test_Charges;
        public decimal Registration_Charges;
        public decimal Total_Bill_Amount;
        public decimal Total_Received;
        public decimal IPDID;
        public decimal Nursing_Charges;
        public decimal Bed_Charges;
        public decimal Surgical_Total;
        public decimal Hospital_Total;
        public decimal Medical_Record_Charges;
        public decimal BioMedical_Waste_Charges;
        public decimal Consultant_Visiting_Charges;
        public decimal Administrative_Charges;
        public decimal Discount;
        public decimal Net_Payable;
        public decimal TotalConsultant;
        public decimal Other_Charges;
        public string Radiology_Total_s;
        public string Lab_Total_s;
        public string SaveFlag;
        public string Branch;
        private SqlConnection con;
        string conString = ConfigurationManager.ConnectionStrings["Connection"].ToString();
        //string conString = ConfigurationSettings.AppSettings("Connection");
        private void connection()
        {
            con = new SqlConnection(conString);
        }
        public List<OPDBillingViewModel>GetOPDBillings()
        {
            connection();
            List<OPDBillingViewModel> OPDBillinglist = new List<OPDBillingViewModel>();
           
                SqlCommand command = new SqlCommand("Sp_GetDataOPDBilling",con);
                command.CommandType = CommandType.StoredProcedure;
               
                SqlDataAdapter sd = new SqlDataAdapter(command);
                DataTable dt = new DataTable();
                con.Open();
                sd.Fill(dt);
                con.Close();
                foreach(DataRow dr in dt.Rows)
                {
                    OPDBillinglist.Add(new OPDBillingViewModel
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
            
                return OPDBillinglist;


        }
        
        public List<OPDBillingViewModel> GetOPDPatientDetails()
        {
            connection();
            List<OPDBillingViewModel> OPDPatientDetails = new List<OPDBillingViewModel>();

            SqlCommand command = new SqlCommand("sp_OpdPatientDetails", con);
            command.CommandType = CommandType.StoredProcedure;

            SqlDataAdapter sd = new SqlDataAdapter(command);
            DataTable dt = new DataTable();
            con.Open();
            sd.Fill(dt);
            con.Close();
            foreach (DataRow dr in dt.Rows)
            {
                OPDPatientDetails.Add(new OPDBillingViewModel
                {

                    Branch = dr["Branch"].ToString(),
                    Patient_ID = dr["Patient_ID"].ToString(),
                    OPDID_Sr = dr["OPDID_Sr"].ToString(),
                    Patient_Name = dr["Patient_Name"].ToString(),
                    Doctor_Name = dr["Doctor_Name"].ToString(),
                    AdmissionDate = Convert.ToDateTime(dr["AdmissionDate"])
                   

                });
            }

            return OPDPatientDetails;


        }



       


        //OPD Fetching Save and Update Accounting table 
        public List<OPDBillingViewModel> ShowAllOPdData()
        {
            connection();
            List<OPDBillingViewModel> OPDPatientDetailsFetching = new List<OPDBillingViewModel>();
            
            con.Open();
            SqlCommand cmb = new SqlCommand(@"Select * from OPD_Patient_Details where Update_flag =0", con);

            SqlDataAdapter adt = new SqlDataAdapter(cmb);
            DataTable dt = new DataTable();
            adt.Fill(dt);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                ShowAllOPdDataFetch();
                ShowACCountingDAtaFetch();
                if (SaveFlag == "")
                {
                    SaveAccountingData();
                    SaveOPdDataUpdate();
                }
                else
                {
                    UpdateAccountingData();
                    SaveOPdDataUpdate();
                    SaveFlag = "";
                }
            }
            return OPDPatientDetailsFetching;
        }
        public List<OPDBillingViewModel> ShowAllOPdDataFetch()
        {
            connection();
            List<OPDBillingViewModel> OPDPatientDetailsFetching = new List<OPDBillingViewModel>();
            //string sqlQuery = @"Select * from OPD_Patient_Details where Update_flag =0";
            SqlCommand cmb = new SqlCommand(@"Select * from OPD_Patient_Details where Update_flag =0", con);
            //SqlCommand command = new SqlCommand(sqlQuery, con);
            //command.CommandType = CommandType.StoredProcedure;

            SqlDataAdapter sd = new SqlDataAdapter(cmb);
            DataTable dt1 = new DataTable();
            con.Open();
            sd.Fill(dt1);
            con.Close();
            if (dt1.Rows.Count > 0)
            {
                OPDID = Convert.ToInt32(dt1.Rows[0]["OPDID"]);
                Date = Convert.ToDateTime(dt1.Rows[0]["Date"]);
                Purpose = Convert.ToString(dt1.Rows[0]["Purpose"]);
                Branch = Convert.ToString(dt1.Rows[0]["Branch"]);
                Procedure_Total = Convert.ToDecimal(dt1.Rows[0]["Procedure_Total"]);
                Lab_Total = Convert.ToDecimal(dt1.Rows[0]["Lab_Total"]);
                Radiology_Total = Convert.ToDecimal(dt1.Rows[0]["Radiology_Total"]);
                Bill_Amount = Convert.ToDecimal(dt1.Rows[0]["Bill_Amount"]);
                Received = Convert.ToDecimal(dt1.Rows[0]["Received"]);
                Pending = Convert.ToDecimal(dt1.Rows[0]["Pending"]);
                Patient_Name = Convert.ToString(dt1.Rows[0]["Patient_Name"]);
                Registration_Charges = Convert.ToDecimal(dt1.Rows[0]["Consultant_Charges"]);
                Consultant_Charges = Convert.ToDecimal(dt1.Rows[0]["Registration_Charges"]);
                Test_Charges = Lab_Total + Radiology_Total;
                Total_Bill_Amount = Consultant_Charges + Registration_Charges + Bill_Amount;
                Total_Received = Consultant_Charges + Registration_Charges + Received;
            }


            return OPDPatientDetailsFetching;


        }
        public void ShowACCountingDAtaFetch()
        {


            connection();
            SqlCommand cmb = new SqlCommand(@"Select * from All_AccountingDetails where PatientID_OPD_IPD=@PatientID_OPD_IPD ", con);
            cmb.Parameters.AddWithValue("@PatientID_OPD_IPD", OPDID);

            SqlDataAdapter adt = new SqlDataAdapter(cmb);
            DataTable dt1 = new DataTable();
            adt.Fill(dt1);
            if (dt1.Rows.Count > 0)
            {
                SaveFlag = Convert.ToString(dt1.Rows[0]["SaveFlag"]);
            }
        }
        public void SaveAccountingData()
        {
            connection();
            con.Open();
            SqlCommand cmd = new SqlCommand(@"Insert into All_AccountingDetails (Branch,PatientID_OPD_IPD,Date,Patient_Name,Purpose,Procedure_Charges,Consultant_Charges,Test_Charges,Other_Charges,Total_Amount,Received,Pending,Bed_Charges,Surgical_Proc_Charges,Discount,Advancce,Net_Payable,SaveFlag)
                                                                          Values(@Branch,@PatientID_OPD_IPD,@Date,@Patient_Name,@Purpose,@Procedure_Charges,@Consultant_Charges,@Test_Charges,@Other_Charges,@Total_Amount,@Received,@Pending,@Bed_Charges,@Surgical_Proc_Charges,@Discount,@Advancce,@Net_Payable,@SaveFlag)", con);
            cmd.Parameters.AddWithValue("@Branch", Branch);
            cmd.Parameters.AddWithValue("@PatientID_OPD_IPD", OPDID);
            cmd.Parameters.AddWithValue("@Date", Date);
            cmd.Parameters.AddWithValue("@Patient_Name", Patient_Name);
            cmd.Parameters.AddWithValue("@Purpose", Purpose);
            cmd.Parameters.AddWithValue("@Procedure_Charges", Procedure_Total);
            cmd.Parameters.AddWithValue("@Consultant_Charges", Consultant_Charges);
            cmd.Parameters.AddWithValue("@Test_Charges", Test_Charges);
            cmd.Parameters.AddWithValue("@Other_Charges", Registration_Charges);
            cmd.Parameters.AddWithValue("@Total_Amount", Total_Bill_Amount);
            cmd.Parameters.AddWithValue("@Received", Total_Received);
            cmd.Parameters.AddWithValue("@Pending", Pending);
            cmd.Parameters.AddWithValue("@Bed_Charges", 0);
            cmd.Parameters.AddWithValue("@Surgical_Proc_Charges", 0);
            cmd.Parameters.AddWithValue("@Discount", 0);
            cmd.Parameters.AddWithValue("@Advancce", 0);
            cmd.Parameters.AddWithValue("@Net_Payable", 0);
            cmd.Parameters.AddWithValue("@SaveFlag", 0);
            cmd.ExecuteNonQuery();
            con.Close();
        }
        public void SaveOPdDataUpdate()
        {
            connection();

            con.Open();
            SqlCommand cmd = new SqlCommand(@"Update OPD_Patient_Details set Update_flag=@Update_flag where OPDID=@OPDID", con);
            cmd.Parameters.AddWithValue("@OPDID", OPDID);
            cmd.Parameters.AddWithValue("@Update_flag", 1);

            cmd.ExecuteNonQuery();
            con.Close();
        }
        public void UpdateAccountingData()
        {
            connection();
            con.Open();

            SqlCommand cmd = new SqlCommand(@"Update All_AccountingDetails set Date=@Date,Patient_Name=@Patient_Name,Purpose=@Purpose,Procedure_Charges=@Procedure_Charges,Consultant_Charges=@Consultant_Charges,Test_Charges=@Test_Charges,Other_Charges=@Other_Charges,Total_Amount=@Total_Amount,Received=@Received,Pending=@Pending,Bed_Charges=@Bed_Charges,Surgical_Proc_Charges=@Surgical_Proc_Charges,Discount=@Discount,Advancce=@Advancce,Net_Payable=@Net_Payable where PatientID_OPD_IPD=@PatientID_OPD_IPD", con);
            cmd.Parameters.AddWithValue("@PatientID_OPD_IPD", OPDID);
            cmd.Parameters.AddWithValue("@Date", Date);
            cmd.Parameters.AddWithValue("@Patient_Name", Patient_Name);
            cmd.Parameters.AddWithValue("@Purpose", Purpose);
            cmd.Parameters.AddWithValue("@Procedure_Charges", Procedure_Total);
            cmd.Parameters.AddWithValue("@Consultant_Charges", Consultant_Charges);
            cmd.Parameters.AddWithValue("@Test_Charges", Test_Charges);
            cmd.Parameters.AddWithValue("@Other_Charges", Registration_Charges);
            cmd.Parameters.AddWithValue("@Total_Amount", Total_Bill_Amount);
            cmd.Parameters.AddWithValue("@Received", Total_Received);
            cmd.Parameters.AddWithValue("@Pending", Pending);
            cmd.Parameters.AddWithValue("@Bed_Charges", 0);
            cmd.Parameters.AddWithValue("@Surgical_Proc_Charges", 0);
            cmd.Parameters.AddWithValue("@Discount", 0);
            cmd.Parameters.AddWithValue("@Advancce", 0);
            cmd.Parameters.AddWithValue("@Net_Payable", 0);
            cmd.ExecuteNonQuery();
            con.Close();
        }


        //IPD Fetching Save and Update Accounting table 
        public List<OPDBillingViewModel> ShowAllIPDData()
        {
            connection();
            List<OPDBillingViewModel> IPDPatientDetailsFetching = new List<OPDBillingViewModel>();
            SqlCommand cmb = new SqlCommand(@"Select * from IPD_Patient_Details where Update_flag =0", con);
            SqlDataAdapter adt = new SqlDataAdapter(cmb);
            DataTable dt = new DataTable();
            adt.Fill(dt);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                ShowAllIPDDataFetch();
                ShowACCountingDAtaFetchIPD();
                if (SaveFlag == "")
                {
                    SaveIPDAccountingData();
                    SaveIPDDataUpdate();
                }
                else
                {
                    UpdateIPDAccountingData();
                    SaveIPDDataUpdate();
                    SaveFlag = "";
                }
            }
            return IPDPatientDetailsFetching;
        }
        public void ShowAllIPDDataFetch()
        {

            connection();
           
            SqlCommand cmb = new SqlCommand(@"Select * from IPD_Patient_Details where Update_flag =0", con);

            SqlDataAdapter adt = new SqlDataAdapter(cmb);
            DataTable dt1 = new DataTable();
            adt.Fill(dt1);
            IPDID = Convert.ToInt32(dt1.Rows[0]["IPD_ID"]);
            Date = Convert.ToDateTime(dt1.Rows[0]["Billing_date"]);
            Branch = Convert.ToString(dt1.Rows[0]["Branch"]);
            Lab_Total_s = Convert.ToString(dt1.Rows[0]["Lab_Total"]);
            Radiology_Total_s = Convert.ToString(dt1.Rows[0]["Radiology_Total"]);
            Registration_Charges = Convert.ToDecimal(dt1.Rows[0]["Registration_Charges"]);
            Consultant_Charges = Convert.ToDecimal(dt1.Rows[0]["Consultant_Charges"]);
            Nursing_Charges = Convert.ToDecimal(dt1.Rows[0]["Nursing_Charges"]);
            Bed_Charges = Convert.ToDecimal(dt1.Rows[0]["Bed_Charges"]);
            Surgical_Total = Convert.ToDecimal(dt1.Rows[0]["Surgical_Total"]);
            Hospital_Total = Convert.ToDecimal(dt1.Rows[0]["Hospital_Total"]);
            Medical_Record_Charges = Convert.ToDecimal(dt1.Rows[0]["Medical_Record_Charges"]);
            BioMedical_Waste_Charges = Convert.ToDecimal(dt1.Rows[0]["BioMedical_Waste_Charges"]);
            Consultant_Visiting_Charges = Convert.ToDecimal(dt1.Rows[0]["Consultant_Visiting_Charges"]);
            Administrative_Charges = Convert.ToDecimal(dt1.Rows[0]["Administrative_Charges"]);
            Bill_Amount = Convert.ToDecimal(dt1.Rows[0]["Bill_Amount"]);
            Discount = Convert.ToDecimal(dt1.Rows[0]["Discount"]);
            Received = Convert.ToDecimal(dt1.Rows[0]["Received"]);
            Pending = Convert.ToDecimal(dt1.Rows[0]["Pending"]);
            Net_Payable = Convert.ToDecimal(dt1.Rows[0]["Net_Payable"]);
            Patient_Name = Convert.ToString(dt1.Rows[0]["Name"]);
            if (Lab_Total_s == "")
            {
                Lab_Total_s = "0";
            }
            if (Radiology_Total_s == "")
            {
                Radiology_Total_s = "0";
            }
            Test_Charges = Convert.ToDecimal(Lab_Total_s) + Convert.ToDecimal(Radiology_Total_s) + 0;
            TotalConsultant = Consultant_Charges + Consultant_Visiting_Charges;
            Other_Charges = Nursing_Charges + Medical_Record_Charges + BioMedical_Waste_Charges + Administrative_Charges + Registration_Charges;
        }
        public void SaveIPDAccountingData()
        {
            connection();
            con.Open();
            SqlCommand cmd = new SqlCommand(@"Insert into All_AccountingDetails (Branch,PatientID_OPD_IPD,Date,Patient_Name,Purpose,Procedure_Charges,Consultant_Charges,Test_Charges,Other_Charges,Total_Amount,Received,Pending,Bed_Charges,Surgical_Proc_Charges,Discount,Advancce,Net_Payable,SaveFlag)
                                                                          Values(@Branch,@PatientID_OPD_IPD,@Date,@Patient_Name,@Purpose,@Procedure_Charges,@Consultant_Charges,@Test_Charges,@Other_Charges,@Total_Amount,@Received,@Pending,@Bed_Charges,@Surgical_Proc_Charges,@Discount,@Advancce,@Net_Payable,@SaveFlag)", con);
            cmd.Parameters.AddWithValue("@Branch", Branch);
            cmd.Parameters.AddWithValue("@PatientID_OPD_IPD", IPDID);
            cmd.Parameters.AddWithValue("@Date", Date);
            cmd.Parameters.AddWithValue("@Patient_Name", Patient_Name);
            cmd.Parameters.AddWithValue("@Purpose", "IPD");
            cmd.Parameters.AddWithValue("@Procedure_Charges", Hospital_Total);
            cmd.Parameters.AddWithValue("@Consultant_Charges", TotalConsultant);
            cmd.Parameters.AddWithValue("@Test_Charges", Test_Charges);
            cmd.Parameters.AddWithValue("@Other_Charges", Other_Charges);
            cmd.Parameters.AddWithValue("@Total_Amount", Bill_Amount);
            cmd.Parameters.AddWithValue("@Received", Received);
            cmd.Parameters.AddWithValue("@Pending", Pending);
            cmd.Parameters.AddWithValue("@Bed_Charges", Bed_Charges);
            cmd.Parameters.AddWithValue("@Surgical_Proc_Charges", Surgical_Total);
            cmd.Parameters.AddWithValue("@Discount", Discount);
            cmd.Parameters.AddWithValue("@Advancce", 0);
            cmd.Parameters.AddWithValue("@Net_Payable", Net_Payable);
            cmd.Parameters.AddWithValue("@SaveFlag", 0);
            cmd.ExecuteNonQuery();
            con.Close();
        }
        public void SaveIPDDataUpdate()
        {
            connection();
            con.Open();
            SqlCommand cmd = new SqlCommand(@"Update IPD_Patient_Details set Update_flag=@Update_flag where IPD_ID=@IPDID", con);
            cmd.Parameters.AddWithValue("@IPDID", IPDID);
            cmd.Parameters.AddWithValue("@Update_flag", 1);
            cmd.ExecuteNonQuery();
            con.Close();
        }
        public void ShowACCountingDAtaFetchIPD()
        {
            connection();
            SqlCommand cmb = new SqlCommand(@"Select * from All_AccountingDetails where PatientID_OPD_IPD=@PatientID_OPD_IPD ", con);
            cmb.Parameters.AddWithValue("@PatientID_OPD_IPD", IPDID);

            SqlDataAdapter adt = new SqlDataAdapter(cmb);
            DataTable dt1 = new DataTable();
            adt.Fill(dt1);
            if (dt1.Rows.Count > 0)
            {
                SaveFlag = Convert.ToString(dt1.Rows[0]["SaveFlag"]);
            }
        }
        public void UpdateIPDAccountingData()
        {
            //connection();
            con.Open();
            SqlCommand cmd = new SqlCommand(@"Update All_AccountingDetails set Date=@Date,Patient_Name=@Patient_Name,Purpose=@Purpose,Procedure_Charges=@Procedure_Charges,Consultant_Charges=@Consultant_Charges,Test_Charges=@Test_Charges,Other_Charges=@Other_Charges,Total_Amount=@Total_Amount,Received=@Received,Pending=@Pending,Bed_Charges=@Bed_Charges,Surgical_Proc_Charges=@Surgical_Proc_Charges,Discount=@Discount,Advancce=@Advancce,Net_Payable=@Net_Payable where PatientID_OPD_IPD=@PatientID_OPD_IPD ", con);
            cmd.Parameters.AddWithValue("@PatientID_OPD_IPD", IPDID);
            cmd.Parameters.AddWithValue("@Date", Date);
            cmd.Parameters.AddWithValue("@Patient_Name", Patient_Name);
            cmd.Parameters.AddWithValue("@Purpose", "IPD");
            cmd.Parameters.AddWithValue("@Procedure_Charges", Hospital_Total);
            cmd.Parameters.AddWithValue("@Consultant_Charges", TotalConsultant);
            cmd.Parameters.AddWithValue("@Test_Charges", Test_Charges);
            cmd.Parameters.AddWithValue("@Other_Charges", Other_Charges);
            cmd.Parameters.AddWithValue("@Total_Amount", Bill_Amount);
            cmd.Parameters.AddWithValue("@Received", Received);
            cmd.Parameters.AddWithValue("@Pending", Pending);
            cmd.Parameters.AddWithValue("@Bed_Charges", Bed_Charges);
            cmd.Parameters.AddWithValue("@Surgical_Proc_Charges", Surgical_Total);
            cmd.Parameters.AddWithValue("@Discount", Discount);
            cmd.Parameters.AddWithValue("@Advancce", 0);
            cmd.Parameters.AddWithValue("@Net_Payable", Net_Payable);
            
            cmd.ExecuteNonQuery();
            con.Close();

        }



        //Test Fetching Save and Update Accounting table
        public List<OPDBillingViewModel> ShowAllOnlyData()
        {
            connection();
            List<OPDBillingViewModel> OnlyTestPatientDetailsFetching = new List<OPDBillingViewModel>();
            SqlCommand cmb = new SqlCommand(@"Select * from OnlyTest_PatientDetails where Update_flag =0", con);

            SqlDataAdapter adt = new SqlDataAdapter(cmb);
            DataTable dt = new DataTable();
            adt.Fill(dt);
            for (int i = 0; i < dt.Rows.Count; i++)
            {

                ShowAllOnlyDataFetch();
                ShowOnlyTestACCountingDAtaFetch();
                if (SaveFlag == "")
                {
                    SaveOnlyAccountingData();
                    SaveOnlyDataUpdate();
                }
                else
                {
                    UpdateOnlyAccountingData();
                    SaveOnlyDataUpdate();
                    SaveFlag = "";
                }
            }
            return OnlyTestPatientDetailsFetching;
        }
        public void ShowAllOnlyDataFetch()
        {

            connection();
            SqlCommand cmb = new SqlCommand(@"Select * from OnlyTest_PatientDetails where Update_flag =0", con);

            SqlDataAdapter adt = new SqlDataAdapter(cmb);
            DataTable dt1 = new DataTable();
            adt.Fill(dt1);
            OPDID = Convert.ToInt32(dt1.Rows[0]["OPDID"]);
            Date = Convert.ToDateTime(dt1.Rows[0]["Billing_Date"]);
            Purpose = Convert.ToString(dt1.Rows[0]["Purpose"]);
            Branch = Convert.ToString(dt1.Rows[0]["Branch"]);
            Lab_Total = Convert.ToDecimal(dt1.Rows[0]["Lab_Total"]);
            Radiology_Total = Convert.ToDecimal(dt1.Rows[0]["Radiology_Total"]);
            Bill_Amount = Convert.ToDecimal(dt1.Rows[0]["Bill_Amount"]);
            Received = Convert.ToDecimal(dt1.Rows[0]["Received"]);
            Pending = Convert.ToDecimal(dt1.Rows[0]["Pending"]);
            Patient_Name = Convert.ToString(dt1.Rows[0]["Patient_Name"]);
            Registration_Charges = Convert.ToDecimal(dt1.Rows[0]["Consultant_Charges"]);
            Consultant_Charges = Convert.ToDecimal(dt1.Rows[0]["Registration_Charges"]);
            Test_Charges = Lab_Total + Radiology_Total;
            Total_Bill_Amount = Consultant_Charges + Registration_Charges + Bill_Amount;
            Total_Received = Consultant_Charges + Registration_Charges + Received;
        }
        public void SaveOnlyAccountingData()
        {
            connection();
            con.Open();
            SqlCommand cmd = new SqlCommand(@"Insert into All_AccountingDetails (Branch,PatientID_OPD_IPD,Date,Patient_Name,Purpose,Procedure_Charges,Consultant_Charges,Test_Charges,Other_Charges,Total_Amount,Received,Pending,Bed_Charges,Surgical_Proc_Charges,Discount,Advancce,Net_Payable,SaveFlag)
                                                                          Values(@Branch,@PatientID_OPD_IPD,@Date,@Patient_Name,@Purpose,@Procedure_Charges,@Consultant_Charges,@Test_Charges,@Other_Charges,@Total_Amount,@Received,@Pending,@Bed_Charges,@Surgical_Proc_Charges,@Discount,@Advancce,@Net_Payable,@SaveFlag)", con);
            cmd.Parameters.AddWithValue("@Branch", Branch);
            cmd.Parameters.AddWithValue("@PatientID_OPD_IPD", OPDID);
            cmd.Parameters.AddWithValue("@Date", Date);
            cmd.Parameters.AddWithValue("@Patient_Name", Patient_Name);
            cmd.Parameters.AddWithValue("@Purpose", Purpose);
            cmd.Parameters.AddWithValue("@Procedure_Charges", 0);
            cmd.Parameters.AddWithValue("@Consultant_Charges", Consultant_Charges);
            cmd.Parameters.AddWithValue("@Test_Charges", Test_Charges);
            cmd.Parameters.AddWithValue("@Other_Charges", Registration_Charges);
            cmd.Parameters.AddWithValue("@Total_Amount", Total_Bill_Amount);
            cmd.Parameters.AddWithValue("@Received", Total_Received);
            cmd.Parameters.AddWithValue("@Pending", Pending);
            cmd.Parameters.AddWithValue("@Bed_Charges", 0);
            cmd.Parameters.AddWithValue("@Surgical_Proc_Charges", 0);
            cmd.Parameters.AddWithValue("@Discount", 0);
            cmd.Parameters.AddWithValue("@Advancce", 0);
            cmd.Parameters.AddWithValue("@Net_Payable", 0);
            cmd.Parameters.AddWithValue("@SaveFlag", 0);
            cmd.ExecuteNonQuery();
            con.Close();
        }
        public void SaveOnlyDataUpdate()
        {
            connection();
            con.Open();
            SqlCommand cmd = new SqlCommand(@"Update OnlyTest_PatientDetails set Update_flag=@Update_flag where OPDID=@OPDID", con);
            cmd.Parameters.AddWithValue("@OPDID", OPDID);
            cmd.Parameters.AddWithValue("@Update_flag", 1);
            cmd.ExecuteNonQuery();
            con.Close();
        }
        public void ShowOnlyTestACCountingDAtaFetch()
        {
            connection();
            SqlCommand cmb = new SqlCommand(@"Select * from All_AccountingDetails where PatientID_OPD_IPD=@PatientID_OPD_IPD ", con);
            cmb.Parameters.AddWithValue("@PatientID_OPD_IPD", OPDID);

            SqlDataAdapter adt = new SqlDataAdapter(cmb);
            DataTable dt1 = new DataTable();
            adt.Fill(dt1);
            if (dt1.Rows.Count > 0)
            {
                SaveFlag = Convert.ToString(dt1.Rows[0]["SaveFlag"]);
            }
        }
        public void UpdateOnlyAccountingData()
        {
            connection();
            con.Open();
            SqlCommand cmd = new SqlCommand(@"Update All_AccountingDetails set Date=@Date,Patient_Name=@Patient_Name,Purpose=@Purpose,Procedure_Charges=@Procedure_Charges,Consultant_Charges=@Consultant_Charges,Test_Charges=@Test_Charges,Other_Charges=@Other_Charges,Total_Amount=@Total_Amount,Received=@Received,Pending=@Pending,Bed_Charges=@Bed_Charges,Surgical_Proc_Charges=@Surgical_Proc_Charges,Discount=@Discount,Advancce=@Advancce,Net_Payable=@Net_Payable where PatientID_OPD_IPD=@PatientID_OPD_IPD", con);
            cmd.Parameters.AddWithValue("@PatientID_OPD_IPD", OPDID);
            cmd.Parameters.AddWithValue("@Date", Date);
            cmd.Parameters.AddWithValue("@Patient_Name", Patient_Name);
            cmd.Parameters.AddWithValue("@Purpose", Purpose);
            cmd.Parameters.AddWithValue("@Procedure_Charges", 0);
            cmd.Parameters.AddWithValue("@Consultant_Charges", Consultant_Charges);
            cmd.Parameters.AddWithValue("@Test_Charges", Test_Charges);
            cmd.Parameters.AddWithValue("@Other_Charges", Registration_Charges);
            cmd.Parameters.AddWithValue("@Total_Amount", Total_Bill_Amount);
            cmd.Parameters.AddWithValue("@Received", Total_Received);
            cmd.Parameters.AddWithValue("@Pending", Pending);
            cmd.Parameters.AddWithValue("@Bed_Charges", 0);
            cmd.Parameters.AddWithValue("@Surgical_Proc_Charges", 0);
            cmd.Parameters.AddWithValue("@Discount", 0);
            cmd.Parameters.AddWithValue("@Advancce", 0);
            cmd.Parameters.AddWithValue("@Net_Payable", 0);
            cmd.ExecuteNonQuery();
            con.Close();
        }

    }
}