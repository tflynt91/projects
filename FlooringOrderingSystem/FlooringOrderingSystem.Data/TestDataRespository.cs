using FlooringOrderingSystem.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlooringOrderingSystem.Data
{
    public class TestDataRepository : IOrderRepository
    {
        

        private IDictionary<string, Product> _products = new Dictionary<string, Product>();
        private IDictionary<string, TaxInfo> _taxInfo = new Dictionary<string, TaxInfo>();
        private Dictionary<DateTime, Order> _orders = new Dictionary<DateTime, Order>();

        public Dictionary<DateTime, int> OrderIndex { get; set; }
        public List<Product> Products { get; set; }

        public const string _taxFilePath = ".\\Taxes.txt";
        public const string _productsFilePath = ".\\Products.txt";
        public const string _ordersFilePath = ".\\Orders_06012013.txt";
        

        public TestDataRepository()
        {
            string[] filePaths = new string[] { _taxFilePath, _productsFilePath, _ordersFilePath };

            foreach (string path in filePaths)
            {
                switch(path)
                {
                    case(_taxFilePath):
                        string[] lines = new string[]
                         {
                           "StateAbbreviation,StateName,TaxRate",
                           "OH,Ohio,6.25",
                           "PA,Pennsylvania,6.75",
                           "MI,Michigan,5.75",
                           "IN,Indiana,6.00"
                        };

                        File.WriteAllLines(path, lines);
                        CreateTaxInfoDictionary(_taxFilePath);
                        break;
                    case (_productsFilePath):

                    string[] lines1 = new string[]
                     {
                         "ProductType,CostPerSquareFoot,LaborCostPerSquareFoot",
                         "Carpet,2.25,2.10",
                         "Laminate,1.75,2.10",
                         "Tile,3.50,4.15",
                         "Wood,5.15,4.75"
                     };

                    File.WriteAllLines(path, lines1);
                    CreateProductDictionary(_productsFilePath);
                        break;
                    case (_ordersFilePath):
                    string[] lines2 = new string[]
                     {
                         "OrderNumber,CustomerName,State,TaxRate,ProductType,Area,CostPerSquareFoot,LaborCostPerSquareFoot,MaterialCost,LaborCost,Tax,Total",
                         "1,Wise,OH,6.25,Wood,100.00,5.15,4.75,515.00,475.00,61.88,1051.88"
                     };

                    File.WriteAllLines(path, lines2);
                    CreateOrderDictionary(_ordersFilePath);
                        break;
                }
            }

        }
        

        private void CreateProductDictionary(string filePath)
        {
            Products = new List<Product>();


            string[] rows = File.ReadAllLines(filePath);           

            for (int i = 1; i < rows.Length; i++)
            {
                string[] columns = rows[i].Split(',');

                Product _product = new Product();

                _product.ProductType = columns[0];
                _product.CostPerSquareFoot = decimal.Parse(columns[1]);
                _product.LaborCostPerSquareFoot = decimal.Parse(columns[2]);

                _products.Add(_product.ProductType, _product);
                Products.Add(_product);
            }
        }

        private void CreateTaxInfoDictionary(string filePath)
        {
            string[] rows = File.ReadAllLines(filePath);

            for (int i = 1; i < rows.Length; i++)
            {
                string[] columns = rows[i].Split(',');

                TaxInfo taxInfo = new TaxInfo();

                taxInfo.StateAbbreviation = columns[0];
                taxInfo.StateName = columns[1];
                taxInfo.TaxRate = decimal.Parse(columns[2]);

                _taxInfo.Add(taxInfo.StateAbbreviation, taxInfo);
            }
        }

        private void CreateOrderDictionary(string filePath)
        {
            OrderIndex = new Dictionary<DateTime, int>();
            int dateTimeIndex = filePath.LastIndexOf("_") + 1;
            string date = filePath.Substring(dateTimeIndex, 8);
            DateTime dateTime = DateTime.ParseExact(date, "MMddyyyy", CultureInfo.InvariantCulture);
            string[] rows = File.ReadAllLines(filePath);
            OrderIndex.Add(dateTime, 0);

            for (int i = 1; i < rows.Length; i++)
            {
                string[] columns = rows[i].Split(',');

                Order _order = new Order();

                _order.OrderNumber = int.Parse(columns[0]);
                _order.CustomerName = columns[1];
                _order.State = columns[2];
                _order.TaxRate = decimal.Parse(columns[3]);
                _order.ProductType = columns[4];
                _order.Area = decimal.Parse(columns[5]);
                _order.CostPerSquareFoot = decimal.Parse(columns[6]);
                _order.LaborCostPerSquareFoot = decimal.Parse(columns[7]);
                _order.MaterialCost = decimal.Parse(columns[7]);
                _order.LaborCost = decimal.Parse(columns[8]);
                _order.Tax = decimal.Parse(columns[9]);
                _order.Total = decimal.Parse(columns[10]);

                _orders.Add(dateTime, _order);

                OrderIndex[dateTime] = _order.OrderNumber;
            }

            
        }
        public List<Order> LoadOrders(DateTime date)
        {
            List<Order> orders = new List<Order>();

            var orders1 = _orders.Where(c => c.Key.Equals(date)).Select(c => c.Value);
            foreach(Order order in orders1)
            {
                orders.Add(order);
            }
            return orders;
        }
        public Order LoadOrder(DateTime date, int orderNumber)
        {
            var order = _orders.Where(c => c.Key.Equals(date)).Select(c => c.Value).Where(d => d.OrderNumber == orderNumber).SingleOrDefault();

            return order;
        }

        public void SaveOrder(Order order, DateTime date)
        {
            string dateToSave = date.ToString("MMddyyyy");

            string[] lines = File.ReadAllLines(_ordersFilePath);

            int lineToWrite = order.OrderNumber;

            using (StreamWriter sw = new StreamWriter(_ordersFilePath))
            {
                for (int currentLine = 1; currentLine <= lines.Length; ++currentLine)
                {
                    if (currentLine == lineToWrite)
                    {
                        sw.WriteLine($"{order.OrderNumber.ToString()},{order.CustomerName},{order.State},{order.TaxRate},{order.ProductType},{order.ProductType},{order.Area},{order.Area},{order.CostPerSquareFoot},{order.LaborCostPerSquareFoot},{order.MaterialCost},{order.LaborCost},{order.Tax},{order.Total}");
                    }
                    else
                    {
                        sw.WriteLine(lines[currentLine]);
                    }
                }
            }
           
            CreateOrderDictionary(_ordersFilePath);
        
        }
        public void DeleteOrder(Order order, DateTime date)
        {
            string[] lines = File.ReadAllLines(_ordersFilePath);

            File.Delete(_ordersFilePath);

            using(StreamWriter sw = File.AppendText(_ordersFilePath))
            {
                for (int currentLine = 0; currentLine <= lines.Length; ++currentLine)
                {
                    if (currentLine == order.OrderNumber)
                    {
                        sw.WriteLine($"{order.OrderNumber.ToString()},{order.CustomerName},{order.State},{order.TaxRate},{order.ProductType},{order.ProductType},{order.Area},{order.Area},{order.CostPerSquareFoot},{order.LaborCostPerSquareFoot},{order.MaterialCost},{order.LaborCost},{order.Tax},{order.Total}");
                    }
                    else
                    {
                        sw.WriteLine(lines[currentLine]);
                    }
                }
            }

            CreateOrderDictionary(_ordersFilePath);
        }

        public TaxInfo LoadTaxInfo(string state)
        {

            TaxInfo taxInfo;

            if (state.Length < 3)
            {
                taxInfo = _taxInfo.Where(c => c.Key.Equals(state)).Select(d => d.Value).SingleOrDefault();
            }
            else
            {
                taxInfo = _taxInfo.Where(c => c.Value.StateName.Equals(state)).Select(d => d.Value).SingleOrDefault();
            }

            if (!(taxInfo == null))
            {
                return taxInfo;
            }

            return null;
        }

        public Product LoadProduct(string productType)
        {
            var productInfo = _products.Where(c => c.Key.Equals(productType)).Select(d => d.Value).SingleOrDefault();

            if (!(productInfo == null))
            {
                return productInfo;
            }
            else
            {
                return null;
            }
        }

        
    }
}
