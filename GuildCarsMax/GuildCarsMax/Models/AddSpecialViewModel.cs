using GuildCarsMax.Models.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GuildCarsMax.Models
{
    public class AddSpecialViewModel
    {
        public Special Special { get; set; }
        public IEnumerable<Special> Specials { get; set; }
    }
}