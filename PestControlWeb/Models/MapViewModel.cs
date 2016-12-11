using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PestControlDll.Entities;

namespace PestControlWeb.Models
{
    public class MapViewModel
    {
        public string Address { get; set; }
        public Route Route { get; set; }
    }
}