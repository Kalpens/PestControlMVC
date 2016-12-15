using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PestControlDll.Entities;
using PestControlDll.Interfaces;

namespace PestControlDll.UnitTest
{
    [TestClass]
    public class TestFacade
    {
        [TestMethod]
        public void TestGetDestinationGateway()
        {
            var service = new DllFacade().GetDestinationServiceGateway();
            Assert.IsNotNull(service);
            Assert.IsInstanceOfType(service, typeof(IServiceGateway<Destination>));
        }
        [TestMethod]
        public void TestGetUserGateway()
        {
            var service = new DllFacade().GetUserServiceGateway();
            Assert.IsNotNull(service);
            Assert.IsInstanceOfType(service, typeof(IServiceGateway<User>));
        }
        [TestMethod]
        public void TestGetRouteGateway()
        {
            var service = new DllFacade().GetRouteServiceGateway();
            Assert.IsNotNull(service);
            Assert.IsInstanceOfType(service, typeof(IServiceGateway<Route>));
        }
        [TestMethod]
        public void TestGetWorksheetGateway()
        {
            var service = new DllFacade().GetWorksheetServiceGateway();
            Assert.IsNotNull(service);
            Assert.IsInstanceOfType(service, typeof(IServiceGateway<Worksheet>));
        }
        [TestMethod]
        public void TestGetPestTypeGateway()
        {
            var service = new DllFacade().GetPestTypeServiceGateway();
            Assert.IsNotNull(service);
            Assert.IsInstanceOfType(service, typeof(IServiceGateway<PestType>));
        }
        [TestMethod]
        public void TestGetAccountGateway()
        {
            var service = new DllFacade().GetAccountGateway();
            Assert.IsNotNull(service);
            Assert.IsInstanceOfType(service, typeof(IAccountGateway));
        }
    }
}
