using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PestControlDll.Entities
{
    class Route : AbstractEntity
    {
        public List<Destination> Destinations { get; set; }
        public DateTime Date { get; set; }
    }
}
