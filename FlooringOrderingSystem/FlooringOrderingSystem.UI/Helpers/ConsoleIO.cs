using FlooringOrderingSystem.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlooringOrderingSystem.UI.Helpers
{
    public class ConsoleIO
    {
        public static void DisplayProducts(List<Product> products)
        {

            

            Console.WriteLine($"{("Product Type"), -15} {("Cost Per Square Foot"), -20} {("Labor Cost Per Square Foot"), -20}");
            Console.WriteLine("------------------------------------------------------------------------------");

            foreach(Product product in products)
            { 

                Console.WriteLine("{0, -15} {1, -20} {2, -20}", product.ProductType, product.CostPerSquareFoot, product.LaborCostPerSquareFoot);
                
            }
        }

        public static void DisplayOrdersDetails(List<Order> Orders, DateTime date)
        {
            foreach(Order order in Orders)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("------------------------------------------");
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine($"{order.OrderNumber} | {date}");
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"{order.CustomerName}");
                Console.WriteLine($"{order.State}");
                Console.ResetColor();
                Console.WriteLine($"Product: {order.ProductType}");
                Console.WriteLine($"Materials: {order.MaterialCost}");
                Console.WriteLine($"Labor: {order.LaborCost}");
                Console.WriteLine($"Tax: {order.Tax}");
                Console.WriteLine($"Total: {order.Total}");
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("------------------------------------------");
            }
        }

        public static void DisplayOrderDetails(Order order, DateTime date)
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine($"{order.OrderNumber} | {date}");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"{order.CustomerName}");
            Console.WriteLine($"{order.State}");
            Console.ResetColor();
            Console.WriteLine($"Product: {order.ProductType}");
            Console.WriteLine($"Materials: {order.MaterialCost}");
            Console.WriteLine($"Labor: {order.LaborCost}");
            Console.WriteLine($"Tax: {order.Tax}");
            Console.WriteLine($"Total: {order.Total}");
        }
    }
}
