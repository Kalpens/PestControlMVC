using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PestControlDll.Entities
{
    class Destination : AbstractEntity
    {
        public Worksheet Worksheet { get; set; }

        //FullAddress, contains ALL info such as, street name, post code, city, country etc...
        public string FullAddress { get; set; }
    }
}
