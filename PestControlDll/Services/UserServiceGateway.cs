using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using PestControlDll.Entities;

namespace PestControlDll.Services
{
    class UserServiceGateway : AbstractPestcontrolServiceGateway, IServiceGateway<User>
    {
        public User Post(User t)
        {
            using (var client = new HttpClient())
            {
                PrepareHeaderWithAuthentication(client);
                var response = client.PostAsJsonAsync("api/users", t).Result;
                if (response.IsSuccessStatusCode)
                {
                    return response.Content.ReadAsAsync<User>().Result;
                }
                return null;
            }
        }

        public User Get(int id)
        {
            using (var client = new HttpClient())
            {
                PrepareHeaderWithAuthentication(client);
                var response = client.GetAsync($"api/users/{id}").Result;
                if (response.IsSuccessStatusCode)
                {
                    User u = response.Content.ReadAsAsync<User>().Result;
                    List<Route> routes = new DllFacade().GetRouteServiceGateway().Get();
                    u.Routes = routes.Where(x => x.UserId == u.Id).ToList();
                    return u;
                }
                return null;
            }
        }

        public List<User> Get()
        {
            using (var client = new HttpClient())
            {
                PrepareHeaderWithAuthentication(client);
                var response = client.GetAsync("api/users").Result;
                if (response.IsSuccessStatusCode)
                {
                    return response.Content.ReadAsAsync<List<User>>().Result;
                }
                return null;
            }
        }

        public User Put(User t)
        {
            using (var client = new HttpClient())
            {
                PrepareHeaderWithAuthentication(client);
                var response = client.PutAsJsonAsync("api/users", t).Result;
                if (response.IsSuccessStatusCode)
                {
                    return response.Content.ReadAsAsync<User>().Result;
                }
                return null;
            }
        }

        public bool Delete(int id)
        {
            using (var client = new HttpClient())
            {
                PrepareHeaderWithAuthentication(client);
                var response = client.DeleteAsync($"api/users/{id}").Result;
                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
                return false;
            }
        }
    }
}
