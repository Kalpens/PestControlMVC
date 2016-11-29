﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using PestControlDll.Entities;

namespace PestControlDll.Services
{
    class DestinationServiceGateway : AbstractPestcontrolServiceGateway, IServiceGateway<Destination>
    {
        public Destination Put(Destination t)
        {
            using (var client = new HttpClient())
            {
                RunAsync(client);
                var response = client.PostAsJsonAsync("api/destinations", t).Result;
                if (response.IsSuccessStatusCode)
                {
                    return response.Content.ReadAsAsync<Destination>().Result;
                }
                return null;
            }
        }

        public Destination Get(int id)
        {
            using (var client = new HttpClient())
            {
                RunAsync(client);
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
                RunAsync(client);
                var response = client.GetAsync("api/destinations").Result;
                if (response.IsSuccessStatusCode)
                {
                    return response.Content.ReadAsAsync<List<Destination>>().Result;
                }
                return null;
            }
        }

        public Destination Post(Destination t)
        {
            using (var client = new HttpClient())
            {
                RunAsync(client);
                var response = client.PutAsJsonAsync("api/destinations", t).Result;
                if (response.IsSuccessStatusCode)
                {
                    return response.Content.ReadAsAsync<Destination>().Result;
                }
                return null;
            }
        }

        public bool Delete(int id)
        {
            using (var client = new HttpClient())
            {
                RunAsync(client);
                var response = client.DeleteAsync($"api/destinations/{id}").Result;
                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
                return false;
            }
        }
    }
}