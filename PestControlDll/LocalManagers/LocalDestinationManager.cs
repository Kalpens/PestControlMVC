using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GoogleMaps.LocationServices;
using PestControlDll.Entities;
using PestControlDll.Interfaces;

namespace PestControlDll.LocalManagers
{
    class LocalDestinationManager : ILocalDestinationManager
    {
        public Destination CreateDestination(Route route, string address)
        {
            Destination destination = new Destination()
            {Route = route, FullAddress = address};
            setCoordinates(destination);
            return destination;
        }
        private void setCoordinates(Destination t)
        {
            var point = new GoogleLocationService().GetLatLongFromAddress(t.FullAddress);
            t.Lat = point.Latitude;
            t.Long = point.Longitude;
        }
    }
}
