using GuildCarsMax.Models.Queries;
using GuildCarsMax.Models.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GuildCarsMax.Models
{
    public class PurchaseVehicleViewModel
    {
        public Sale Sale { get; set; }
        public VehicleInventoryListingDetails Vehicle { get; set; }
        public IEnumerable<PurchaseType> PurchaseTypes { get; set; }
        public IEnumerable<State> States { get; set; }

    }
}