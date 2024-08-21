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
    public class LoginEmployeeController : Controller
    {
        private SqlConnection con;
        string conString = ConfigurationManager.ConnectionStrings["Connection"].ToString();
        //string conString = ConfigurationSettings.AppSettings("Connection");
        private void connection()
        {
            con = new SqlConnection(conString);
        }
        LoginDetailsDBHandler dBHandler = new LoginDetailsDBHandler();
        // GET: LoginEmployee
        public ActionResult Index()
        {
            ModelState.Clear();

            return View(dBHandler.GetDataLogin());
        }

        // GET: LoginEmployee/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: LoginEmployee/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: LoginEmployee/Create
        [HttpPost]
        public ActionResult Create(LoginModel smodel)
        {
            try
            {
               if(ModelState.IsValid)
                {
                    if(dBHandler.EmployeeDetails(smodel))
                    {
                        
                        ViewBag.Message="Record Added Successfully";
                        ModelState.Clear();
                    }
                }
                //return RedirectToAction("Index");
                return View();
            }
            catch
            {
                return View();
            }
        }
        //[HttpGet]
        public ActionResult LoginDetails(string User_Name, string Password)
        {
            try
            {
                string user_name = User_Name;
                string password_details = Password;

                if (User_Name == null && Password == null)
                {
                    ViewBag.ErrorMessage = "Invalid login attempt";
                    return View();



                }
                else
                {
                    connection();
                    string sqlQuery = @"select  User_Name,Password from Login_Details where User_Name='" + user_name + "' and Password='" + password_details + "'";
                    SqlCommand command = new SqlCommand(sqlQuery, con);
                    con.Open();
                    SqlDataAdapter sd = new SqlDataAdapter(command);
                    DataTable dt = new DataTable();
                    sd.Fill(dt);
                    if (dt.Rows.Count > 0)
                        return RedirectToAction("Index", "HMS_Global");
                    else
                        ViewBag.ErrorMessage = "Invalid login attempt";
                    return View();

                }


                return View();
            }
            catch
            {
                return View();
            }
        }
        
        public ActionResult Createnew(LoginModel smodel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (dBHandler.logindetails(smodel))
                    {
                        ViewBag.Message = "login successfully";
                        ModelState.Clear();
                        return RedirectToAction("Index", "HMS_Global");
                    }
                    else
                    {
                        //ViewBag.Message = "Please Enter Valied Username and Password";
                    }
                }

                //return RedirectToAction("Index");
                return View();
            }
            catch
            {
                return View();
            }
        }
        // GET: LoginEmployee/Edit/5
        public ActionResult Edit(int id)
        {
            return View(dBHandler.GetDataLogin().Find(smodel=>smodel.GID==id));
        }

        // POST: LoginEmployee/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, LoginModel smodel)
        {
            try
            {
                dBHandler.UpdatePassword(smodel);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: LoginEmployee/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: LoginEmployee/Delete/5
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
