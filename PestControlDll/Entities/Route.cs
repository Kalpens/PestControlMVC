using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PestControlDll.Entities
{
    public class Route : AbstractEntity
    {
        public string Name { get; set; }
        public List<Destination> Destinations { get; set; }
        public DateTime Date { get; set; }
        public User User { get; set; }
        public int UserId { get; set; }
    }
}
