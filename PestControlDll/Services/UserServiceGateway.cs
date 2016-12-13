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

                response.EnsureSuccessStatusCode();

                return response.Content.ReadAsAsync<User>().Result;
            }
        }

        public User Get(int id)
        {
            using (var client = new HttpClient())
            {
                PrepareHeaderWithAuthentication(client);
                var response = client.GetAsync($"api/users/{id}").Result;

                response.EnsureSuccessStatusCode();

                User u = response.Content.ReadAsAsync<User>().Result;
                List<Route> routes = new DllFacade().GetRouteServiceGateway().Get();
                u.Routes = routes.Where(x => x.UserId == u.Id).ToList();
                return u;
            }
        }

        public List<User> Get()
        {
            using (var client = new HttpClient())
            {
                PrepareHeaderWithAuthentication(client);
                var response = client.GetAsync("api/users").Result;
                response.EnsureSuccessStatusCode();

                    List<User> userlist = new List<User>();
                    //A loop which adds routes to each user.
                    foreach (var item in response.Content.ReadAsAsync<List<User>>().Result)
                    {
                        List<Route> routes = new DllFacade().GetRouteServiceGateway().Get();
                        item.Routes = routes.Where(x => x.UserId == item.Id).ToList();
                        userlist.Add(item);
                    }
                    return userlist;
            }
        }

        public User Put(User t)
        {
            using (var client = new HttpClient())
            {
                PrepareHeaderWithAuthentication(client);
                var response = client.PutAsJsonAsync("api/users", t).Result;

                response.EnsureSuccessStatusCode();

                return response.Content.ReadAsAsync<User>().Result;
            }
        }

        public bool Delete(int id)
        {
            using (var client = new HttpClient())
            {
                PrepareHeaderWithAuthentication(client);
                var response = client.DeleteAsync($"api/users/{id}").Result;

                response.EnsureSuccessStatusCode();

                return true;
            }
        }
    }
}
