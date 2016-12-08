using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PestControlDll.Entities
{
    public class Destination : AbstractEntity
    {
        public Worksheet Worksheet { get; set; }

        //FullAddress, contains ALL info such as, street name, post code, city, country etc...
        public string FullAddress { get; set; }
        public Route Route { get; set; }
        public int RouteId { get; set; }
        public double Lat { get; set; }
        public double Long { get; set; }
    }
}
