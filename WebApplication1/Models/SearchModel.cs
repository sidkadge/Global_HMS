﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class SearchModel
    {
        public string  Branch{ get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public List<OPDBillingViewModel> SearchResults { get; set; }
    }
}