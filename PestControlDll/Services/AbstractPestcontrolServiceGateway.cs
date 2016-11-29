using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace PestControlDll.Services
{
    class AbstractPestcontrolServiceGateway
    {
        public async Task RunAsync(HttpClient client)
        {
            client.BaseAddress = new Uri("http://hmswebapisolution.azurewebsites.net/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }
    }
}
