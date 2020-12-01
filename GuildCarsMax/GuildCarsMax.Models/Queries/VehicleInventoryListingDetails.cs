using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuildCarsMax.Models.Queries
{
    public class VehicleInventoryListingDetails
    {
        public string VinNumber { get; set; }
        public int ModelTypeId { get; set; }
        public string ModelType { get; set; }
        public int MakeTypeId { get; set; }
        public string MakeType { get; set; }
        public int BodyStyleId { get; set; }
        public string BodyStyle { get; set; }
        public int InteriorColorId { get; set; }
        public string InteriorColor { get; set; }
        public int ExteriorColorId { get; set; }
        public string ExteriorColor { get; set; }
        public int TransmissionTypeId { get; set; }
        public string TransmissionType { get; set; }
        public string ImageFileName { get; set; }
        public decimal MSRP { get; set; }
        public int Mileage { get; set; }
        public decimal SalePrice { get; set; }
        public int Year { get; set; }
        public string VehicleDescription { get; set; }
    }
}
