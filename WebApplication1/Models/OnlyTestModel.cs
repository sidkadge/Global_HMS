using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class OnlyTestModel
    {
       
        public string OPDID_Sr { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public Nullable<System.DateTime> Date { get; set; }
        [Required]
        [DataType(DataType.Date)]

        public Nullable<System.DateTime> Billing_Date { get; set; }
        public string Branch { get; set; }
        public string Purpose { get; set; }
        public Nullable<decimal> Lab_Total { get; set; }
        public Nullable<decimal> Radiology_Total { get; set; }
        public Nullable<decimal> Bill_Amount { get; set; }
        public Nullable<decimal> Received { get; set; }
        public Nullable<decimal> Pending { get; set; }
        public string Patient_Name { get; set; }
      
        public string Doctor_Name { get; set; }
        public Nullable<decimal> Consultant_Charges { get; set; }
        public Nullable<decimal> Registration_Charges { get; set; }
        public string Update_flag { get; set; }
    }
}