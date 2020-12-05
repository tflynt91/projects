using GuildCarsMax.Models.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GuildCarsMax.Models
{
    public class InventoryReportViewModel
    {
        public IEnumerable<InventoryReportRow> NewVehicles { get; set; }
        public IEnumerable<InventoryReportRow> UsedVehicles { get; set; }
    }
}