using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuildCarsMax.Models.Queries
{
    public class SalesReportFilterParameters
    {
        public string UserId { get; set; }
        public string User { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }

    }
}
