using GuildCars.Models.Queries;
using GuildCars.Models.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GuildCars.UI.Models
{
    public class HomeViewModel
    {
        public IEnumerable<Special> Specials { get; set; }
        public IEnumerable<VehicleShortItem> FeaturedVehicles { get; set; }

    }
}