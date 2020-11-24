using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuildCars.Models.Tables
{
    public class ModelType
    {
        public int ModelTypeId { get; set; }
        public int MakeTypeId { get; set; }
        public string ModelTypeName { get; set; }
        public DateTime DateAdded { get; set; }
        public string UserId { get; set; }

    }
}
