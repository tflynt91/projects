using GuildCarsMax.Models.Queries;
using GuildCarsMax.Models.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GuildCarsMax.Models
{
    public class AddMakeViewModel
    {
        public IEnumerable<AddMakeDetail> Makes { get; set;}
        public AddMakeParameters MakeParameters { get; set; }

    }
}