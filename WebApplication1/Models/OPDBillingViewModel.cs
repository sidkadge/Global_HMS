using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class OPDBillingViewModel
    {
        public string  Branch { get; set; }
        public string Patient_ID { get; set; }
        public string OPDID_Sr { get; set; }
        public string Patient_Name { get; set; }
        public string Doctor_Name { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime AdmissionDate { get; set; }
        
        public decimal Bill_Amount { get; set; }
        public decimal Received { get; set; }
        public decimal Pending { get; set; }
        public decimal Lab_Total { get; set; }
        public decimal Radiology_Total { get; set; }
        public decimal Procedure_Total { get; set; }
        public decimal Consultant_Charges { get; set; }
        public decimal Registration_Charges { get; set; }


        [Required]
        [DataType(DataType.Date)]
        public DateTime FromDate { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime ToDate { get; set; }

    }
}