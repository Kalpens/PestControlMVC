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
                PrepareHeaderWithAuthentication(client);
                var response = client.PostAsJsonAsync("api/routes", t).Result;

                response.EnsureSuccessStatusCode();

                return response.Content.ReadAsAsync<Route>().Result;
            }
        }

        public Route Get(int id)
        {
            using (var client = new HttpClient())
            {
                PrepareHeaderWithAuthentication(client);
                var response = client.GetAsync($"api/routes/{id}").Result;

                response.EnsureSuccessStatusCode();

                Route r = response.Content.ReadAsAsync<Route>().Result;
                List<Destination> destinations = new DllFacade().GetDestinationServiceGateway().Get();
                r.Destinations = destinations.Where(x => x.RouteId == r.Id).ToList();
                return r;
            }
        }

        public List<Route> Get()
        {
            using (var client = new HttpClient())
            {
                PrepareHeaderWithAuthentication(client);
                var response = client.GetAsync("api/routes").Result;

                response.EnsureSuccessStatusCode();

                List<Route> routelist = new List<Route>();

                    //A loop which adds routes to each user.
                    foreach (var item in response.Content.ReadAsAsync<List<Route>>().Result)
                    {
                        List<Destination> destinations = new DllFacade().GetDestinationServiceGateway().Get();
                        item.Destinations = destinations.Where(x => x.RouteId == item.Id).ToList();
                        routelist.Add(item);
                    }
                    return routelist;
            }
        }

        public Route Put(Route t)
        {
            using (var client = new HttpClient())
            {
                PrepareHeaderWithAuthentication(client);
                var response = client.PutAsJsonAsync("api/routes", t).Result;

                response.EnsureSuccessStatusCode();

                return response.Content.ReadAsAsync<Route>().Result;
            }
        }

        public bool Delete(int id)
        {
            using (var client = new HttpClient())
            {
                PrepareHeaderWithAuthentication(client);
                var response = client.DeleteAsync($"api/routes/{id}").Result;

                response.EnsureSuccessStatusCode();

                return true;
            }
        }
    }
}
