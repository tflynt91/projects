using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuildCars.Models.Queries
{
    public class VehicleShortItem
    {
        public string VinNumber { get; set; }
        public string ModelType { get; set; }
        public string MakeType { get; set; }
        public string ImageFileName { get; set; }
        public decimal SalePrice { get; set; }
        public int Year { get; set; }
    }
}
