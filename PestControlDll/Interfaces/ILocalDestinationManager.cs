using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PestControlDll.Entities;

namespace PestControlDll.Interfaces
{
    public interface ILocalDestinationManager
    {
        /// <summary>
        /// Creates local destination with coordinates.
        /// </summary>
        /// <param name="address"></param>
        /// <returns></returns>
        Destination CreateDestination(Route route, string address);
    }
}
