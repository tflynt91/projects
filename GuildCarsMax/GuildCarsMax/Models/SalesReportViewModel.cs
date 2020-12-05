using GuildCarsMax.Models.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GuildCarsMax.Models
{
    public class SalesReportViewModel
    {
        public SalesReportFilterParameters FilterParameters { get; set; }
        public IEnumerable<SelectListItem> Users { get; set; }
        public IEnumerable<SalesReportSearchResult> SalesReport { get; set; }
    }
}