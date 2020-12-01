using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuildCarsMax.Models.Queries
{
    public class InventoryReportRow
    {
        public int Year { get; set; }
        public string ModelType { get; set; }
        public string MakeType { get; set; }
        public int Count { get; set; }
        public decimal StockValue { get; set; }

    }
}
