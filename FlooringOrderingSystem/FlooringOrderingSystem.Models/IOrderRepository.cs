using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlooringOrderingSystem.Models
{
    public interface IOrderRepository
    {
        Order LoadOrder(DateTime date, int orderNumber);
        void SaveOrder(Order order, DateTime date);
        void DeleteOrder(Order order, DateTime date);
        TaxInfo LoadTaxInfo(string stateAbbreviation);
        Product LoadProduct(string productType);

        List<Order> LoadOrders(DateTime date);

        Dictionary<DateTime, int> OrderIndex { get; set; }

        List<Product> Products { get; set; }

        
    }
}
