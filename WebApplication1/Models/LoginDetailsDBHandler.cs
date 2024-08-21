using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class LoginDetailsDBHandler
    {
        private SqlConnection con;
        String Constring = ConfigurationManager.ConnectionStrings["Connection"].ToString();
        private void Connection()
        {
            con = new SqlConnection(Constring);
        }

        public bool EmployeeDetails(LoginModel smodel)
        {
            Connection();
            SqlCommand cmd = new SqlCommand("Sp_SaveEMpDetails", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Name", smodel.Name);
            cmd.Parameters.AddWithValue("@Designation", smodel.Designation);
            cmd.Parameters.AddWithValue("@Branch", smodel.Branch);
            cmd.Parameters.AddWithValue("@Email_ID", smodel.Email_ID);
            cmd.Parameters.AddWithValue("@User_Name", smodel.User_Name);
            cmd.Parameters.AddWithValue("@Password", smodel.Password);
            cmd.Parameters.AddWithValue("@Status", smodel.Status);
            con.Open();
                int i = cmd.ExecuteNonQuery();
            con.Close();
            if(i>=1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public List<LoginModel>GetDataLogin()
        {
            List<LoginModel> LoginList = new List<LoginModel>();
            Connection();
            SqlCommand smd = new SqlCommand("Sp_GetLoginData",con);
            smd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter sd = new SqlDataAdapter(smd);
            DataTable td = new DataTable();
           
            con.Open();
            sd.Fill(td);
            con.Close();
             foreach(DataRow dr in td.Rows)
            {
                LoginList.Add(new LoginModel
                {
                    GID = Convert.ToInt32(dr["GID"]),
                    Name = Convert.ToString(dr["Name"]),
                    Designation = Convert.ToString(dr["Designation"]),
                    Branch = Convert.ToString(dr["Branch"]),
                    Email_ID = Convert.ToString(dr["Email_ID"]),
                    User_Name = Convert.ToString(dr["User_Name"]),
                    Password = Convert.ToString(dr["Password"])


                });
                
            }
            return LoginList;


        }

        public bool UpdatePassword(LoginModel smodel)
        {
            Connection();
            SqlCommand cmd = new SqlCommand("Sp_UpdatePassword", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@GID", smodel.GID);
            cmd.Parameters.AddWithValue("@Name", smodel.Name);
            cmd.Parameters.AddWithValue("@Designation", smodel.Designation);
            cmd.Parameters.AddWithValue("@Branch", smodel.Branch);
            cmd.Parameters.AddWithValue("@Email_ID", smodel.Email_ID);
            cmd.Parameters.AddWithValue("@User_Name", smodel.User_Name);
            cmd.Parameters.AddWithValue("@Password", smodel.Password);
            con.Open();
            int i = cmd.ExecuteNonQuery();
            con.Close();
            if(i >=1)
            {
                return true;

            }
            else
            {
                return false;
            }

        }
        public bool logindetails(LoginModel smodel)
        {
            String user_name;
            String password_details;
            Connection();
            SqlCommand cmd = new SqlCommand("Sp_Savelogin", con);
            cmd.CommandType = CommandType.StoredProcedure;
           
            cmd.Parameters.AddWithValue("@User_Name", smodel.User_Name);
            cmd.Parameters.AddWithValue("@Password", smodel.Password);
            user_name = smodel.User_Name;
            password_details = smodel.Password;


            string sqlQuery = @"select  User_Name,Password from Login_Details where User_Name='" + user_name + "' and Password='" + password_details + "'";
            SqlCommand command = new SqlCommand(sqlQuery, con);
            con.Open();
            SqlDataAdapter sd = new SqlDataAdapter(command);
            DataTable dt = new DataTable();
            sd.Fill(dt);
            
            con.Close();
            if (dt.Rows.Count >= 1)
            {
                return true;
            }
            else
            {
                return false;
            }


            
        }



    }
}