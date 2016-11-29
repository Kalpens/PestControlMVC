using System;
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
        public Route Post(Route t)
        {
            using (var client = new HttpClient())
            {
                RunAsync(client);
                var response = client.PostAsJsonAsync("api/routes", t).Result;
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
                var response = client.GetAsync($"api/routes/{id}").Result;
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
                var response = client.GetAsync("api/routes").Result;
                if (response.IsSuccessStatusCode)
                {
                    return response.Content.ReadAsAsync<List<Route>>().Result;
                }
                return null;
            }
        }

        public Route Put(Route t)
        {
            using (var client = new HttpClient())
            {
                RunAsync(client);
                var response = client.PutAsJsonAsync("api/routes", t).Result;
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
                var response = client.DeleteAsync($"api/routes/{id}").Result;
                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
                return false;
            }
        }
    }
}
