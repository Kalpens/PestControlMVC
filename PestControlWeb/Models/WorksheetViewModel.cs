using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PestControlDll.Entities;

namespace PestControlWeb.Models
{
    public class WorksheetViewModel
    {
        public Worksheet Worksheet { get; set; }
        public List<PestType> PestTypes { get; set; }

    }
}