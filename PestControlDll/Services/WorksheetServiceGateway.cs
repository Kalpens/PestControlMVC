using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using PestControlDll.Entities;

namespace PestControlDll.Services
{
    class WorksheetServiceGateway : AbstractPestcontrolServiceGateway, IServiceGateway<Worksheet>
    {
        public Worksheet Post(Worksheet t)
        {
            using (var client = new HttpClient())
            {
                PrepareHeaderWithAuthentication(client);
                var response = client.PostAsJsonAsync("api/worksheets", t).Result;
                if (response.IsSuccessStatusCode)
                {
                    return response.Content.ReadAsAsync<Worksheet>().Result;
                }
                return null;
            }
        }

        public Worksheet Get(int id)
        {
            using (var client = new HttpClient())
            {
                PrepareHeaderWithAuthentication(client);
                var response = client.GetAsync($"api/worksheets/{id}").Result;
                if (response.IsSuccessStatusCode)
                {
                    return response.Content.ReadAsAsync<Worksheet>().Result;
                }
                return null;
            }
        }

        public List<Worksheet> Get()
        {
            using (var client = new HttpClient())
            {
                PrepareHeaderWithAuthentication(client);
                var response = client.GetAsync("api/worksheets").Result;
                if (response.IsSuccessStatusCode)
                {
                    return response.Content.ReadAsAsync<List<Worksheet>>().Result;
                }
                return null;
            }
        }

        public Worksheet Put(Worksheet t)
        {
            using (var client = new HttpClient())
            {
                PrepareHeaderWithAuthentication(client);
                var response = client.PutAsJsonAsync("api/worksheets", t).Result;
                if (response.IsSuccessStatusCode)
                {
                    return response.Content.ReadAsAsync<Worksheet>().Result;
                }
                return null;
            }
        }

        public bool Delete(int id)
        {
            using (var client = new HttpClient())
            {
                PrepareHeaderWithAuthentication(client);
                var response = client.DeleteAsync($"api/worksheets/{id}").Result;
                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
                return false;
            }
        }
    }
}
