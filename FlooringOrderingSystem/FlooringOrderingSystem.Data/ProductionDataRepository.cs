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
    public class ProductionDataRepository : IOrderRepository
    {
        public Dictionary<DateTime, int> OrderIndex { get; set; }
        public List<Product> Products { get; set; }

        public const string _taxFilePath = ".\\Taxes.txt";
        public const string _productsFilePath = ".\\Products.txt";
        public const string _ordersFilePath = ".\\Orders";


        private IDictionary<string, Product> _products = new Dictionary<string, Product>();
        private IDictionary<string, TaxInfo> _taxInfo = new Dictionary<string, TaxInfo>();
        private Dictionary<string, Order> _orders;

        public ProductionDataRepository()
        {
            string[] filePaths = new string[] { _taxFilePath, _productsFilePath, _ordersFilePath };

            foreach (string path in filePaths)
            {
                switch (path)
                {
                    case (_taxFilePath):
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
                        if (!Directory.Exists(_ordersFilePath))
                        {
                            Directory.CreateDirectory(_ordersFilePath);
                        }
                        CreateOrderDictionary();
                        break;
                }
            }
        }

        private void CreateOrderDictionary()
        {
            string[] files = Directory.GetFiles(_ordersFilePath);

            OrderIndex = new Dictionary<DateTime, int>();
            _orders = new Dictionary<string, Order>();

            foreach (string file in files)
            {
                int dateTimeIndex = file.LastIndexOf("_") + 1;
                string date = file.Substring(dateTimeIndex, 8);
                DateTime dateTime = DateTime.ParseExact(date, "MMddyyyy", CultureInfo.InvariantCulture);
                string[] rows = File.ReadAllLines(file);
                OrderIndex.Add(dateTime, 0);

                for (int i = 1; i < rows.Length; i++)
                {
                    if (string.IsNullOrEmpty(rows[i]))
                    {
                        continue;
                    }
                    else
                    {
                        string[] Columns = rows[i].Split(',');

                        Order _order = new Order();

                        _order.OrderNumber = int.Parse(Columns[0]);
                        _order.CustomerName = Columns[1];
                        _order.State = Columns[2];
                        _order.TaxRate = decimal.Parse(Columns[3]);
                        _order.ProductType = Columns[4];
                        _order.Area = decimal.Parse(Columns[5]);
                        _order.CostPerSquareFoot = decimal.Parse(Columns[6]);
                        _order.LaborCostPerSquareFoot = decimal.Parse(Columns[7]);
                        _order.MaterialCost = decimal.Parse(Columns[7]);
                        _order.LaborCost = decimal.Parse(Columns[8]);
                        _order.Tax = decimal.Parse(Columns[9]);
                        _order.Total = decimal.Parse(Columns[10]);

                        _orders.Add($"{dateTime}_{_order.OrderNumber}", _order);

                        OrderIndex[dateTime] = _order.OrderNumber;
                    }
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

        public Order LoadOrder(DateTime date, int orderNumber)
        {
            var order = _orders.Where(c => c.Key.Contains(date.ToString())).Select(c => c.Value).Where(d => d.OrderNumber == orderNumber).SingleOrDefault();

            return order;
        }

        public void SaveOrder(Order order, DateTime date)
        {
            string dateToSave = date.ToString("MMddyyyy");
            string[] files = Directory.GetFiles(_ordersFilePath);

            var fileToSave = files.Where(c => c.Contains(dateToSave)).FirstOrDefault();

            if (File.Exists(fileToSave))
            {
                string[] lines = File.ReadAllLines(fileToSave);

                File.Delete(fileToSave);

                int lineToWrite = order.OrderNumber;

                using (StreamWriter sw = new StreamWriter(fileToSave))
                {
                    for (int currentLine = 0; currentLine <= lines.Length; currentLine++)
                    {
                        if (currentLine == lineToWrite)
                        {
                            sw.WriteLine($"{order.OrderNumber.ToString()},{order.CustomerName},{order.State},{order.TaxRate},{order.ProductType},{order.Area},{order.CostPerSquareFoot},{order.LaborCostPerSquareFoot},{order.MaterialCost},{order.LaborCost},{order.Tax},{order.Total}");
                        }
                        else
                        {
                            sw.WriteLine(lines[currentLine]);
                        }
                    }
                }
                
            }
            else
            {


                string path = $"\\Orders_{dateToSave}.txt";
                string[] lines = new string[]
                {
                    "OrderNumber,CustomerName,State,TaxRate,ProductType,Area,CostPerSquareFoot,LaborCostPerSquareFoot,MaterialCost,LaborCost,Tax,Total",
                    $"{order.OrderNumber.ToString()},{order.CustomerName},{order.State},{order.TaxRate},{order.ProductType},{order.Area},{order.CostPerSquareFoot},{order.LaborCostPerSquareFoot},{order.MaterialCost},{order.LaborCost},{order.Tax},{order.Total}"
                };

                File.WriteAllLines(Path.Combine(_ordersFilePath, $"Orders_{dateToSave}.txt"), lines);
            }

            CreateOrderDictionary();
        }

        public void DeleteOrder(Order order, DateTime date)
        {
            string dateToDelete = date.ToString("MMddyyyy");

            var fileToDelete = Directory.GetFiles(_ordersFilePath).Where(c => c.Contains(dateToDelete)).FirstOrDefault();

            string[] lines = File.ReadAllLines(fileToDelete);

            File.Delete(fileToDelete);

            using (StreamWriter sw = File.AppendText(fileToDelete))
            {
                for (int currentLine = 0; currentLine < lines.Length; currentLine++)
                {
                    if (currentLine == order.OrderNumber)
                    {
                        sw.WriteLine("");
                    }
                    else
                    {
                        sw.WriteLine(lines[currentLine]);
                    }
                }
            }

            CreateOrderDictionary();
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

        public List<Order> LoadOrders(DateTime date)
        {
            List<Order> orders = new List<Order>();

            var orders1 = _orders.Where(c => c.Key.Contains(date.ToString())).Select(c => c.Value);
            foreach (Order order in orders1)
            {
                orders.Add(order);
            }
            return orders;
        }
    }

 }