using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PestControlDll.Entities
{
    class User : AbstractEntity
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public List<Route> Routes { get; set; }
        public string LicensePlate { get; set; }

        //Enums associated its value with ordered numbers, starting from 0.
        //UserType == 0 means that there are Admin.
        public enum UserType { Admin, Default };
    }
}
