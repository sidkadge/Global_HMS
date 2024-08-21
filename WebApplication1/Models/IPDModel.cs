using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class IPDModel
    {
        public string Branch { get; set; }
        public string Patient_ID_sr { get; set; }
        public string IPD_ID_Sr { get; set; }
        public string Name { get; set; }
        public string Doctor_name { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime Admission_Date { get; set; }
       
        public int GID { get; set; }
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
        public string Patient_ID { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime? Discharge_Date { get; set; }
        public decimal Bill_Amount { get; set; }

       
        public decimal Registration_Charges { get; set; }
        public decimal Nursing_Charges { get; set; }
        public decimal Hospital_Total { get; set; }
        public decimal Surgical_Total { get; set; }
        public decimal Medical_Record_Charges { get; set; }
        public decimal BioMedical_Waste_Charges { get; set; }
        public decimal Consultant_Visiting_Charges { get; set; }
        public decimal Administrative_Charges { get; set; }
        [Required]
        [DataType(DataType.Date)]

        public DateTime Billing_date { get; set; }
        public decimal Lab_Total { get; set; }
        public decimal Radiology_Total { get; set; }
        public string PaymentMode { get; set; }

        public string ErrorMsg { get; set; }
    }

}