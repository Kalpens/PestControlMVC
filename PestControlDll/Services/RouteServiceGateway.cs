﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using PestControlDll.Entities;

namespace PestControlDll.Services
{
    class RouteServiceGateway : AbstractPestcontrolServiceGateway, IServiceGateway<Route>
    {
        public Route Put(Route t)
        {
            using (var client = new HttpClient())
            {
                RunAsync(client);
                var response = client.PostAsJsonAsync("api/route", t).Result;
                if (response.IsSuccessStatusCode)
                {
                    return response.Content.ReadAsAsync<Route>().Result;
                }
                return null;
            }
        }

        public Route Get(int id)
        {
            using (var client = new HttpClient())
            {
                RunAsync(client);
                var response = client.GetAsync($"api/route/{id}").Result;
                if (response.IsSuccessStatusCode)
                {
                    return response.Content.ReadAsAsync<Route>().Result;
                }
                return null;
            }
        }

        public List<Route> Get()
        {
            using (var client = new HttpClient())
            {
                RunAsync(client);
                var response = client.GetAsync("api/route").Result;
                if (response.IsSuccessStatusCode)
                {
                    return response.Content.ReadAsAsync<List<Route>>().Result;
                }
                return null;
            }
        }

        public Route Post(Route t)
        {
            using (var client = new HttpClient())
            {
                RunAsync(client);
                var response = client.PutAsJsonAsync("api/route", t).Result;
                if (response.IsSuccessStatusCode)
                {
                    return response.Content.ReadAsAsync<Route>().Result;
                }
                return null;
            }
        }

        public bool Delete(int id)
        {
            using (var client = new HttpClient())
            {
                RunAsync(client);
                var response = client.DeleteAsync($"api/route/{id}").Result;
                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
                return false;
            }
        }
    }
}
