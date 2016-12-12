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
                var u = new UserServiceGateway().Get(t.UserId);
                if (u != null)
                {
                    t.Destinations = new List<Destination>();
                    new UserServiceGateway().Put(u);
                }
                PrepareHeaderWithAuthentication(client);
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
                PrepareHeaderWithAuthentication(client);
                var response = client.GetAsync($"api/routes/{id}").Result;
                if (response.IsSuccessStatusCode)
                {
                    Route r = response.Content.ReadAsAsync<Route>().Result;
                    List<Destination> destinations = new DllFacade().GetDestinationServiceGateway().Get();
                    r.Destinations = destinations.Where(x => x.RouteId == r.Id).ToList();
                    return r;
                }
                return null;
            }
        }

        public List<Route> Get()
        {
            using (var client = new HttpClient())
            {
                PrepareHeaderWithAuthentication(client);
                var response = client.GetAsync("api/routes").Result;
                if (response.IsSuccessStatusCode)
                {
                    List<Route> routelist = new List<Route>();
                    //A loop which adds routes to each user.
                    foreach (var item in response.Content.ReadAsAsync<List<Route>>().Result)
                    {
                        List<Destination> destinations = new DllFacade().GetDestinationServiceGateway().Get();
                        item.Destinations = destinations.Where(x => x.RouteId == item.Id).ToList();
                    }
                    return routelist;
                }
                return null;
            }
        }

        public Route Put(Route t)
        {
            using (var client = new HttpClient())
            {
                PrepareHeaderWithAuthentication(client);
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
                PrepareHeaderWithAuthentication(client);
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
