using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuildCars.Models.Queries
{
    public class AddMakeParameters
    {
        public int MakeTypeId { get; set; }
        public string MakeType { get; set; }
        public string UserId { get; set; }
    }
}
