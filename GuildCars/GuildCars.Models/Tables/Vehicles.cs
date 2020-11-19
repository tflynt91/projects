using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuildCars.Models.Tables
{
    public class Vehicles
    {
        public int VinNumber { get; set; }
        public int ModelTypeId { get; set; }
        public int BodyStyleId { get; set; }
        public int InteriorColorId { get; set; }
        public int ExteriorColorId { get; set; }
        public int TransmissionTypeId { get; set; }
        public int NewOrUsedTypeId { get; set; }
        public string ImageFileName { get; set; }
        public decimal MSRP { get; set; }
        public int Mileage { get; set; }
        public decimal SalePrice { get; set; }
        public DateTime Year { get; set; }
        public string VehicleDescription { get; set; }
        public bool Sold { get; set; }
        public bool Featured { get; set; }


    }
}
