using FlooringOrderingSystem;
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
    public class AddOrderWorkflow
    {
        private bool validDate = false;
        private bool validName = false;
        private bool validState = false;
        private bool validProduct;

        private DateTime _orderDate;
        private string _customerName;
        private TaxInfo _taxInfo;
        private Product _productType;
        private decimal _area;

        public void Execute()
        {
            
            OrderManager manager = OrderManagerFactory.Create();

            Console.Clear();

            while(validDate != true)
            {
                DateTime date = Helpers.Helpers.GetOrderDate("Enter date for order(must be after today): ");
                validDate = AddOrder.CheckDateValidity(date);
                if (validDate == true)
                {
                    _orderDate = date;
                    break;
                }
            }

            _customerName = Helpers.Helpers.GetValidatedNameFromUser("Enter name for order(can only contain letters, numbers or \".\" and \",\"): ");

            while(validState != true)
            {
                string state = Helpers.Helpers.GetRequiredStringFromUser("Enter state name(use abberviation ie. OH or NY): ");
                StateLookupResponse stateResponse = manager.StateLookup(state);
                if(stateResponse.Success)
                {
                    validState = true;
                    _taxInfo = stateResponse.TaxInfo;
                    break;
                }
                else
                {
                    Console.WriteLine(stateResponse.Message);
                    continue;
                }
            }

            

            while (validProduct != true)
            {
                Helpers.ConsoleIO.DisplayProducts(manager.GetProducts());

                string product = Helpers.Helpers.GetRequiredStringFromUser("Please choose which product you would like to order: ");

                ProductLookupResponse productResponse = manager.ProductLookup(product);

                if(productResponse.Success)
                {
                    validProduct = true;
                    _productType = productResponse.Product;
                    break;
                }
                else
                {
                    Console.WriteLine(productResponse.Message);
                    continue;
                }
            }

           _area = Helpers.Helpers.GetRequiredDecimalFromUser("Enter the area size of order(must be at least 100 square feet): ");

           Order newOrder = manager.CreateOrder(_orderDate, _customerName, _taxInfo, _productType, _area);

            Helpers.ConsoleIO.DisplayOrderDetails(newOrder, _orderDate);

            string orderConfirmation = Helpers.Helpers.GetYesNoAnswerFromUser("Would you like to place the order");

            if(orderConfirmation == "Y")
            {
                manager.SaveOrder(newOrder, _orderDate);
            }
            else
            {
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
            }
        }
    }
}
