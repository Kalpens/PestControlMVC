using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Configuration;

namespace PestControlDll.Services
{
    class AbstractPestcontrolServiceGateway
    {
        protected void PrepareHeaderWithAuthentication(HttpClient client)
        {
            client.BaseAddress = new Uri(WebConfigurationManager.AppSettings["PestControlAPIWebsiteUrl"]);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            if (HttpContext.Current.Session["token"] != null)
            {
                string token = HttpContext.Current.Session["token"].ToString();
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
            }
        }

    protected void PrepareHeader(HttpClient client)
        {
            client.BaseAddress = new Uri(WebConfigurationManager.AppSettings["PestControlAPIWebsiteUrl"]);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }
    }
}
