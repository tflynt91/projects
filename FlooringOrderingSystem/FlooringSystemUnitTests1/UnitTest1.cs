using System;
using System.ComponentModel.Design;
using FlooringOrderingSystem;
using FlooringOrderingSystem.Data;
using FlooringOrderingSystem.Models;
using FlooringOrderingSystem.Models.Responses;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework;

namespace FlooringSystemUnitTests1
{
    [TestFixture]
    public class UnitTest1
    {
        [TestCase("OH", true)]
        [TestCase("Indiana", true)]
        [TestCase("MI", true)]
        [TestCase("NY", false)]
        public void StateLookupLoadsCorrectState(string state, bool expectedResult)
        {
            OrderManager manager = OrderManagerFactory.Create();

            StateLookupResponse response = manager.StateLookup(state);

            NUnit.Framework.Assert.AreEqual(expectedResult, response.Success);
        }

        [TestCase("Carpet", true)]
        [TestCase("Wood", true)]
        [TestCase("Cement", false)]
        public void ProductLookupLoadsCorrectProduct(string product, bool expectedResult)
        {
            OrderManager manager = OrderManagerFactory.Create();

            ProductLookupResponse response = manager.ProductLookup(product);

            NUnit.Framework.Assert.AreEqual(expectedResult, response.Success);
        }

        [TestCase("06/01/2013", 1, "Wise")]
        public void OrderLoadsCorrectCustomerName(DateTime date, int orderNumber, string customerName)
        {
            IOrderRepository orderRespository = new TestDataRepository();
            Order order = orderRespository.LoadOrder(date, orderNumber);
            NUnit.Framework.Assert.AreEqual(order.CustomerName, customerName);
        }

        [TestCase("06/01/2013", 1)]
        public void DatabaseLoadsCorrectAmountOfOrders(DateTime date, int numberOfOrders)
        {
            IOrderRepository orderRespository = new TestDataRepository();
            var orders = orderRespository.LoadOrders(date);
            NUnit.Framework.Assert.AreEqual(orders.Count, numberOfOrders);
        }
    }
}
