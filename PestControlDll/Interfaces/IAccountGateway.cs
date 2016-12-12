using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using PestControlDll.Entities;

namespace PestControlDll.Interfaces
{
    public interface IAccountGateway
    {
        HttpResponseMessage Register(string email, string password, string confirmPassword);
        HttpResponseMessage Login(string userName, string password);
        User GetCurrentUser();
    }
}
