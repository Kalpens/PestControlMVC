﻿using System;
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
                var r = new RouteServiceGateway().Get(t.RouteId);
                //If this route exists in db, then it will be added 
                //to destination and also a new worksheet will be added for this destination
                if (r != null)
                {
                    t.Route = r;
                    Worksheet worksheet = new Worksheet() {Destination = t, Address = t.FullAddress};
                    t.Worksheet = new WorksheetServiceGateway().Post(worksheet);
                    //Coordinates being set for destination
                    setCoordinates(t);
                }
                PrepareHeaderWithAuthentication(client);
                var response = client.PostAsJsonAsync("api/destinations", t).Result;
                if (response.IsSuccessStatusCode)
                {
                    r.Destinations.Add(t);
                    new RouteServiceGateway().Put(r);
                    return response.Content.ReadAsAsync<Destination>().Result;
                }
                return null;
            }
        }

        public Destination Get(int id)
        {
            using (var client = new HttpClient())
            {
                PrepareHeaderWithAuthentication(client);
                var response = client.GetAsync($"api/destinations/{id}").Result;
                if (response.IsSuccessStatusCode)
                {
                    return response.Content.ReadAsAsync<Destination>().Result;
                }
                return null;
            }
        }

        public List<Destination> Get()
        {
            using (var client = new HttpClient())
            {
                PrepareHeaderWithAuthentication(client);
                var response = client.GetAsync("api/destinations").Result;
                if (response.IsSuccessStatusCode)
                {
                    return response.Content.ReadAsAsync<List<Destination>>().Result;
                }
                return null;
            }
        }

        public Destination Put(Destination t)
        {
            using (var client = new HttpClient())
            {
                PrepareHeaderWithAuthentication(client);
                var response = client.PutAsJsonAsync("api/destinations", t).Result;
                if (response.IsSuccessStatusCode)
                {
                    setCoordinates(t);
                    return response.Content.ReadAsAsync<Destination>().Result;
                }
                return null;
            }
        }

        public bool Delete(int id)
        {
            using (var client = new HttpClient())
            {
                PrepareHeaderWithAuthentication(client);
                var response = client.DeleteAsync($"api/destinations/{id}").Result;
                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
                return false;
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
