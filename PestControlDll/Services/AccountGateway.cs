using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Newtonsoft.Json.Linq;
using PestControlDll.Entities;
using PestControlDll.Interfaces;

namespace PestControlDll.Services
{
    class AccountGateway : AbstractPestcontrolServiceGateway, IAccountGateway
    {
        public HttpResponseMessage Register(string email, string password, string confirmPassword)
        {
            
            using (var client = new HttpClient())
            {
                PrepareHeader(client);
                var model = new { Email = email, Password = password, ConfirmPassword = confirmPassword };
                HttpResponseMessage response = client.PostAsJsonAsync("api/account/register", model).Result;
                return response;
            }
        }

        public HttpResponseMessage Login(string userName, string password)
        {
            using (var client = new HttpClient())
            {
                PrepareHeader(client);
                //setup login data
                var formContent = new FormUrlEncodedContent(new[]
                {
                    new KeyValuePair<string, string>("grant_type", "password"),
                    new KeyValuePair<string, string>("username", userName),
                    new KeyValuePair<string, string>("password", password)
                });

                //Request token
                HttpResponseMessage response = client.PostAsync("/token", formContent).Result;

                if (response.IsSuccessStatusCode)
                {
                    var responseJson = response.Content.ReadAsStringAsync().Result;
                    var jObject = JObject.Parse(responseJson);
                    string token = jObject.GetValue("access_token").ToString();
                    HttpContext.Current.Session["token"] = token;
                }

                return response;
            }
        }

        public User GetCurrentUser()
        {
            using (var client = new HttpClient())
            {
                PrepareHeaderWithAuthentication(client);
                var response = client.GetAsync($"api/Account/UserInfo").Result;
                response.EnsureSuccessStatusCode();
                var userInfo = response.Content.ReadAsAsync<UserInfo>().Result;
                    User user = GetUserByEmail(userInfo.Email);
                return user ?? null;
            }
        }

        private User GetUserByEmail(string email)
        {
            return new UserServiceGateway().Get().FirstOrDefault(x => x.Email == email);
        }
    }
}
