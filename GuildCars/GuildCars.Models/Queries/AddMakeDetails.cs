using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuildCars.Models.Queries
{
    public class AddMakeDetails
    {
        public int MakeTypeId { get; set; }
        public string MakeType { get; set; }
        public DateTime DateAdded { get; set; }
        public string UserId { get; set; }
        public string Email { get; set; }

    }
}
