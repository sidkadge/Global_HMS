using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;
using System.Data.SqlClient;
using System.Configuration;

namespace WebApplication1.Controllers
{
    public class LoginModelController : Controller
    {
        private SqlConnection con;
        string conString = ConfigurationManager.ConnectionStrings["Connection"].ToString();
        //string conString = ConfigurationSettings.AppSettings("Connection");
        private void Connection()
        {
            con = new SqlConnection(conString);
        }

        [HttpGet]
        public ActionResult Login(LoginModel lm)
        {

           // Connection();

           // string sqlquery = "select  User_Name,Password from Login_Details where User_Name=@User_Name and Password=@Password";
           ///// SqlCommand cmd = new SqlCommand("Sp_Savelogin", con);
           
           // con.Open();
           // SqlCommand cmd = new SqlCommand(sqlquery, con);
           // cmd.Parameters.AddWithValue("@User_Name", lm.User_Name);
           // cmd.Parameters.AddWithValue("@Password", lm.Password);
           // if()
           // SqlDataReader sdr = cmd.ExecuteReader();
           // if(sdr.Read())
           // {
           //     Session["User_Name"] = lm.User_Name.ToString();
           //     return RedirectToAction("Index", "HMS_Global", null);

           // }
           // else
           // {
           //     ViewData["Message"] = "user login details faild";
           // }
           // con.Close();
            return View();
        }
       
    }
}