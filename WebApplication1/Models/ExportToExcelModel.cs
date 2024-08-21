using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
   
    public class ExportToExcelModel
    {
        public int GID { get; set; }
        public string Branch { get; set; }
        [DisplayName("Patient ID")]
        public string PatientID_OPD_IPD { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }
        public string Patient_Name { get; set; }
        public string Purpose { get; set; }
        public decimal Total_Amount { get; set; }
        public decimal Discount { get; set; }
        public decimal Advancce { get; set; }
        public decimal Net_Payable { get; set; }
        public decimal Received { get; set; }
        public decimal Pending { get; set; }
        public decimal Test_Charges { get; set; }
        public decimal Bed_Charges { get; set; }
        public decimal Surgical_Proc_Charges { get; set; }
        public decimal Procedure_Charges { get; set; }
        public decimal Consultant_Charges { get; set; }
        public decimal Other_Charges { get; set; }
        
    }
}