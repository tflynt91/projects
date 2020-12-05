using GuildCarsMax.Models.Queries;
using GuildCarsMax.Models.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GuildCarsMax.Models
{
    public class AddModelViewModel
    {
        public IEnumerable<SelectListItem> Makes { get; set; }
        public AddModelParameters ModelParameters { get; set; }

        public IEnumerable<AddModelDetail> Models { get; set; }
    }
}