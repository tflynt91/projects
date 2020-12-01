using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuildCarsMax.Models.Queries
{
    public class SalesReportSearchResult
    {
        public string User { get; set; }
        public decimal TotalSales { get; set; }
        public int TotalVehicles { get; set; }

    }
}
