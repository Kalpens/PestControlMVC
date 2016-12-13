using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PestControlDll.Entities;
using PestControlDll.Interfaces;
using PestControlDll.Services;

namespace PestControlDll
{
    public class DllFacade
    {
        public IServiceGateway<Destination> GetDestinationServiceGateway()
        {
            return new DestinationServiceGateway();
        }

        public IServiceGateway<PestType> GetPestTypeServiceGateway()
        {
            return new PestTypeServiceGateway();
        }

        public IServiceGateway<Route> GetRouteServiceGateway()
        {
            return new RouteServiceGateway();
        }

        public IServiceGateway<User> GetUserServiceGateway()
        {
            return new UserServiceGateway();
        }
        public IServiceGateway<Worksheet> GetWorksheetServiceGateway()
        {
            return new WorksheetServiceGateway();
        }
        public IAccountGateway GetAccountGateway()
        {
            return new AccountGateway();
        }
    }
}
