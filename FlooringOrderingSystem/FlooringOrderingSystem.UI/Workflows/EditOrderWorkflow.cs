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
    public class EditOrderWorkflow
    {

        public void Execute()
        {
            OrderManager manager = OrderManagerFactory.Create();
            Order _orderToEdit;
            DateTime _orderDate;
            string _editedName;
            TaxInfo _editedTaxInfo;
            Product _editedProduct;
            decimal _editedArea;


            Console.Clear();

            while (true)
            {
                _orderDate = Helpers.Helpers.GetOrderDate("Enter date for order you wish to edit: ");

                Console.WriteLine("Enter order number you would like to edit: ");
                int orderNumber = int.Parse(Console.ReadLine());

                LookupOrderResponse response = manager.LookupOrder(_orderDate, orderNumber);

                if (response.Success)
                {
                    _orderToEdit = response.Order;

                    ConsoleIO.DisplayOrderDetails(response.Order, _orderDate);
                    break;
                }
                else
                {
                    Console.WriteLine("An error occured: ");
                    Console.WriteLine(response.Message);
                }
            }
            while (true)
            {
                Console.WriteLine($"Enter customer name({_orderToEdit.CustomerName}): ");
                string nameInput = Console.ReadLine();
                if (string.IsNullOrEmpty(nameInput))
                {
                    _editedName = _orderToEdit.CustomerName;
                    break;
                }
                else
                {
                    bool validName = AddOrder.CheckNameValidity(nameInput);
                    if (validName == true)
                    {
                        _editedName = nameInput;
                        break;
                    }
                    continue;
                }
            }

            while (true)
            {
                Console.WriteLine($"Enter state name({_orderToEdit.State}): ");

                string state = Console.ReadLine();
                string stateToLookup;

                if (String.IsNullOrEmpty(state))
                {
                    stateToLookup = _orderToEdit.State;
                }
                else
                {
                    stateToLookup = state;
                }

                StateLookupResponse stateResponse = manager.StateLookup(stateToLookup);

                if (stateResponse.Success)
                {

                    _editedTaxInfo = stateResponse.TaxInfo;
                    break;
                }
                else
                {
                    Console.WriteLine(stateResponse.Message);
                    continue;
                }
            }

            while (true)
            {
                Console.WriteLine($"Enter product name ({_orderToEdit.ProductType}): ");

                string product = Console.ReadLine();
                string productToLookup;

                if(string.IsNullOrEmpty(product))
                {
                    productToLookup = _orderToEdit.ProductType;
                }
                else
                {
                    productToLookup = product;
                }

                ProductLookupResponse productResponse = manager.ProductLookup(productToLookup);

                if (productResponse.Success)
                {
                    
                    _editedProduct = productResponse.Product;
                    break;
                }
                else
                {
                    Console.WriteLine(productResponse.Message);
                    continue;
                }
            }

            
            _editedArea = Helpers.Helpers.GetRequiredDecimalFromUser("Enter the area size of order(must be at least 100 square feet): ");

            Order newOrder = manager.CreateOrder(_orderDate, _editedName, _editedTaxInfo, _editedProduct, _editedArea);

            Helpers.ConsoleIO.DisplayOrderDetails(newOrder, _orderDate);

            string orderConfirmation = Helpers.Helpers.GetYesNoAnswerFromUser("Would you like to save the edited order");

            if (orderConfirmation == "Y")
            {
                manager.DeleteOrder(_orderToEdit, _orderDate);
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
