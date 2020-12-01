using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuildCarsMax.Models.Tables
{
    public class MakeType
    {
        public int MakeTypeId { get; set; }
        public string MakeTypeName { get; set; }
        public DateTime DateAdded { get; set; }
        public string UserId { get; set; }

    }
}
