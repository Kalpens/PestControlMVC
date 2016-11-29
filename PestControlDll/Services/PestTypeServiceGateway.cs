using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using PestControlDll.Entities;

namespace PestControlDll.Services
{
    class PestTypeServiceGateway : AbstractPestcontrolServiceGateway, IServiceGateway<PestType>
    {
        public PestType Post(PestType t)
        {
            using (var client = new HttpClient())
            {
                RunAsync(client);
                var response = client.PostAsJsonAsync("api/pesttype", t).Result;
                if (response.IsSuccessStatusCode)
                {
                    return response.Content.ReadAsAsync<PestType>().Result;
                }
                return null;
            }
        }

        public PestType Get(int id)
        {
            using (var client = new HttpClient())
            {
                RunAsync(client);
                var response = client.GetAsync($"api/pesttype/{id}").Result;
                if (response.IsSuccessStatusCode)
                {
                    return response.Content.ReadAsAsync<PestType>().Result;
                }
                return null;
            }
        }

        public List<PestType> Get()
        {
            using (var client = new HttpClient())
            {
                RunAsync(client);
                var response = client.GetAsync("api/pesttype").Result;
                if (response.IsSuccessStatusCode)
                {
                    return response.Content.ReadAsAsync<List<PestType>>().Result;
                }
                return null;
            }
        }

        public PestType Put(PestType t)
        {
            using (var client = new HttpClient())
            {
                RunAsync(client);
                var response = client.PutAsJsonAsync("api/pesttype", t).Result;
                if (response.IsSuccessStatusCode)
                {
                    return response.Content.ReadAsAsync<PestType>().Result;
                }
                return null;
            }
        }

        public bool Delete(int id)
        {
            using (var client = new HttpClient())
            {
                RunAsync(client);
                var response = client.DeleteAsync($"api/pesttype/{id}").Result;
                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
                return false;
            }
        }
    }
}
