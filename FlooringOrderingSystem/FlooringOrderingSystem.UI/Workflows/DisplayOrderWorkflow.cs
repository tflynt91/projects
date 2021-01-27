using FlooringOrderingSystem.Models.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlooringOrderingSystem.UI.Workflows
{
    public class DisplayOrderWorkflow
    {
        public void Execute()
        {
            OrderManager manager = OrderManagerFactory.Create();

            Console.Clear();
            Console.WriteLine("Display Orders");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("------------------------------------------");
            Console.ResetColor();
            DateTime date = Helpers.Helpers.GetOrderDate("Enter a date to display order from(MM/DD/YYYY): ");

            DisplayOrderResponse response = manager.DisplayOrders(date);

            if(response.Success)
            {
                Helpers.ConsoleIO.DisplayOrdersDetails(response.Orders, date);
            }
            else
            {
                Console.WriteLine("An error occured: ");
                Console.WriteLine(response.Message);
            }

            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }
    }
}
