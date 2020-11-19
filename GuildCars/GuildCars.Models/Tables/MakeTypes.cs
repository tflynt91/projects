using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuildCars.Models.Tables
{
    public class MakeTypes
    {
        public int MakeTypeId { get; set; }
        public string MakeType { get; set; }
        public DateTime DateAdded { get; set; }
        public string UserId { get; set; }

    }
}
