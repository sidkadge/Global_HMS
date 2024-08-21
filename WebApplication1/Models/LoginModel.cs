using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class LoginModel
    {
        public int GID { get; set; }
        public string Name { get; set; }
        public string Designation { get; set; }
        public string Branch { get; set; }
        public string Email_ID { get; set; }
        [StringLength(20, MinimumLength = 3)]
        //[Required(ErrorMessage = "Please enter Username")]
        public string User_Name { get; set; }
        //[StringLength(100, ErrorMessage = "Password \"{0}\" must have {2} characters", MinimumLength = 8)]
       // [RegularExpression(@"^([a-zA-Z0-9@*#]{8,15})$", ErrorMessage = "Password must contain: Minimum 8 characters, at least 1 UpperCase Alphabet, 1 LowerCase Alphabet, 1 Number, and 1 Special Character")]
        //[Required(ErrorMessage = "Please enter Password")]
        public string Password { get; set; }
        public string Status { get; set; }

    }
}