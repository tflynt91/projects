using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuildCars.Models.Queries
{
    public class AddModelParameters
    {
        public int ModelTypeId { get; set; }
        public string ModelType { get; set; }
        public int MakeTypeId { get; set; }
        public string UserId { get; set; }

    }
}
