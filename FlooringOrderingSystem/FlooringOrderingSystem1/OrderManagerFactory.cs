using System;
using System.Collections.Generic;
using System.Linq;
using System.Configuration;
using System.Text;
using System.Threading.Tasks;
using FlooringOrderingSystem.Data;

namespace FlooringOrderingSystem
{
    public static class OrderManagerFactory
    {
        public static OrderManager Create()
        {
            string mode = ConfigurationManager.AppSettings["Mode"].ToString();

            switch (mode)
            {
                case "TestMode":
                    return new OrderManager(new TestDataRepository());
                case "ProdMode":
                    return new OrderManager(new ProductionDataRepository());
                default:
                    throw new Exception("Mode value in app config is not valid");
            }
        }
    }
}
