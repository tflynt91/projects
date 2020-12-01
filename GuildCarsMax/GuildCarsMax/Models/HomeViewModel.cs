using GuildCarsMax.Models.Queries;
using GuildCarsMax.Models.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GuildCarsMax.UI.Models
{
    public class HomeViewModel
    {
        public IEnumerable<Special> Specials { get; set; }
        public IEnumerable<VehicleShortItem> FeaturedVehicles { get; set; }

    }
}