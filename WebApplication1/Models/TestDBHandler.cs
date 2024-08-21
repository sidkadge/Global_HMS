using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class TestDBHandler
    {
        
            private SqlConnection con;
            string conString = ConfigurationManager.ConnectionStrings["Connection"].ToString();
            //string conString = ConfigurationSettings.AppSettings("Connection");
            private void connection()
            {
                con = new SqlConnection(conString);
            }
        public List<OnlyTestModel> GetOnlyTest()
        {
            connection();
            List<OnlyTestModel> OnlyTestlist = new List<OnlyTestModel>();

            SqlCommand command = new SqlCommand("Sp_GetOnlyTestBilling", con);
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
                    OnlyTestlist.Add(new OnlyTestModel
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
            return OnlyTestlist;
        }
        
    }
}