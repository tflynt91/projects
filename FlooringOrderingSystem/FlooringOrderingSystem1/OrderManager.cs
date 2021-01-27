using FlooringOrderingSystem.Models;
using FlooringOrderingSystem.Models.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlooringOrderingSystem
{
    public class OrderManager
    {
        private IOrderRepository _orderRespository;
        
        public OrderManager(IOrderRepository orderRepository)
        {
            _orderRespository = orderRepository;
        }

        public DisplayOrderResponse DisplayOrders(DateTime date)
        {
            DisplayOrderResponse response = new DisplayOrderResponse();

            response.Orders = _orderRespository.LoadOrders(date);
            if(response.Orders == null)
            {
                response.Success = false;
                response.Message = "Date is not valid.";
            }
            else
            {
                response.Success = true;
            }

            return response;
        }

        public List<Product> GetProducts()
        {
            return _orderRespository.Products;
        }

        public LookupOrderResponse LookupOrder(DateTime date, int orderNumber)
        {
            LookupOrderResponse response = new LookupOrderResponse();

            response.Order = _orderRespository.LoadOrder(date, orderNumber);

            if(response.Order == null)
            {
                response.Success = false;
                response.Message = $"{date} or {orderNumber} were not valid";
            }
            else
            {
                response.Success = true;
            }

            return response;
        }

        public StateLookupResponse StateLookup(string state)
        {
            StateLookupResponse response = new StateLookupResponse();

            response.TaxInfo = _orderRespository.LoadTaxInfo(state);

            if(response.TaxInfo == null)
            {
                response.Success = false;
                response.Message = $"{state} does not exist in Tax File. Please enter valid state.";
            }
            else
            {
                response.Success = true;
            }

            return response;
        }

        public ProductLookupResponse ProductLookup(string productType)
        {
            ProductLookupResponse response = new ProductLookupResponse();

            response.Product = _orderRespository.LoadProduct(productType);
            
                if(response.Product == null)
                {
                    response.Success = false;
                    response.Message = $"{productType} is not a valid product type.";
                }
                else
                {
                    response.Success = true;
                }

                return response;
            
        }
        public Order CreateOrder(DateTime orderDate, string customerName, TaxInfo taxInfo, Product product, decimal area)
        {
            Order newOrder = new Order();

            if (_orderRespository.OrderIndex.ContainsKey(orderDate))
            {
                newOrder.OrderNumber = _orderRespository.OrderIndex[orderDate] + 1;
            }
            else
            {
                newOrder.OrderNumber = 1;
            }
            newOrder.CustomerName = customerName;
            newOrder.State = taxInfo.StateName;
            newOrder.TaxRate = taxInfo.TaxRate;
            newOrder.ProductType = product.ProductType;
            newOrder.Area = area;
            newOrder.CostPerSquareFoot = product.CostPerSquareFoot;
            newOrder.LaborCostPerSquareFoot = product.LaborCostPerSquareFoot;
            newOrder.MaterialCost = (area * product.CostPerSquareFoot);
            newOrder.LaborCost = (area * product.LaborCostPerSquareFoot);
            newOrder.Tax = Decimal.Round(((newOrder.MaterialCost + newOrder.LaborCost) * (taxInfo.TaxRate / 100)), 2);
            newOrder.Total = Decimal.Round((newOrder.MaterialCost + newOrder.LaborCost + newOrder.Tax), 2);

            return newOrder;

        }

        public void SaveOrder(Order order, DateTime date)
        {
            _orderRespository.SaveOrder(order, date);
        }

        public void DeleteOrder(Order order, DateTime date)
        {
            _orderRespository.DeleteOrder(order, date);
        }
    }
}
