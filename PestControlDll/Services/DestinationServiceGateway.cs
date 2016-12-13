using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using GoogleMaps.LocationServices;
using PestControlDll.Entities;

namespace PestControlDll.Services
{
    class DestinationServiceGateway : AbstractPestcontrolServiceGateway, IServiceGateway<Destination>
    {
        public Destination Post(Destination t)
        {
            using (var client = new HttpClient())
            {
                setCoordinates(t);
                PrepareHeaderWithAuthentication(client);

                var response = client.PostAsJsonAsync("api/destinations", t).Result;

                response.EnsureSuccessStatusCode();

                var destination = response.Content.ReadAsAsync<Destination>().Result;
                //If destination is succesfully created, then we also will create worksheet.
                Worksheet worksheet = new Worksheet() { Address = destination.FullAddress, DestinationId = destination.Id};
                destination.Worksheet = new WorksheetServiceGateway().Post(worksheet);
                return destination;
            }
        }

        public Destination Get(int id)
        {
            using (var client = new HttpClient())
            {
                PrepareHeaderWithAuthentication(client);
                var response = client.GetAsync($"api/destinations/{id}").Result;

                response.EnsureSuccessStatusCode();

                return response.Content.ReadAsAsync<Destination>().Result;
            }
        }

        public List<Destination> Get()
        {
            using (var client = new HttpClient())
            {
                PrepareHeaderWithAuthentication(client);
                var response = client.GetAsync("api/destinations").Result;

                response.EnsureSuccessStatusCode();

                return response.Content.ReadAsAsync<List<Destination>>().Result;
            }
        }

        public Destination Put(Destination t)
        {
            using (var client = new HttpClient())
            {
                PrepareHeaderWithAuthentication(client);
                var response = client.PutAsJsonAsync("api/destinations", t).Result;

                response.EnsureSuccessStatusCode();

                setCoordinates(t);
                return response.Content.ReadAsAsync<Destination>().Result;
            }
        }

        public bool Delete(int id)
        {
            using (var client = new HttpClient())
            {
                PrepareHeaderWithAuthentication(client);
                var response = client.DeleteAsync($"api/destinations/{id}").Result;
                response.EnsureSuccessStatusCode();
                    return true;
            }
        }

        private void setCoordinates(Destination t)
        {
            var point = new GoogleLocationService().GetLatLongFromAddress(t.FullAddress);
            t.Lat = point.Latitude;
            t.Long = point.Longitude;
        }
    }
}
