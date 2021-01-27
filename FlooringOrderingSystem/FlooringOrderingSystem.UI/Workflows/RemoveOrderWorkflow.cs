using FlooringOrderingSystem.Models;
using FlooringOrderingSystem.Models.Responses;
using FlooringOrderingSystem.UI.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlooringOrderingSystem.UI.Workflows
{
    public class RemoveOrderWorkflow
    {
        public void Execute()
        {
            DateTime _orderDate;
            Order _orderToDelete;
            OrderManager manager = OrderManagerFactory.Create();

            Console.Clear();

            while (true)
            {
                _orderDate = Helpers.Helpers.GetOrderDate("Enter date for order you wish to delete: ");

                Console.WriteLine("Enter order number you would like to delete: ");
                int orderNumber = int.Parse(Console.ReadLine());

                LookupOrderResponse response = manager.LookupOrder(_orderDate, orderNumber);

                if (response.Success)
                {
                    _orderToDelete = response.Order;

                    
                    break;
                }
                else
                {
                    Console.WriteLine("An error occured: ");
                    Console.WriteLine(response.Message);
                }
            }

            ConsoleIO.DisplayOrderDetails(_orderToDelete, _orderDate);

            string input = Helpers.Helpers.GetYesNoAnswerFromUser("Are you sure you want to delete this order?");

            if(input == "Y")
            {
                manager.DeleteOrder(_orderToDelete, _orderDate);
            }
            else
            {
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
            }

        }
    }
}
