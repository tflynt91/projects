using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuildCarsMax.Models.Tables
{
    public class Sale
    {
 
        public int SalesId { get; set; }
        public string UserId { get; set; }
        public string VinNumber { get; set; }
        [Required]
        public decimal PurchasePrice { get; set; }
        public DateTime PurchaseDate { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        [Required]
        public string Street1 { get; set; }
        public string Street2 { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public string StateId { get; set; }
        [Required]
        public string ZipCode { get; set; }
        [Required]
        public int PurchaseTypeId { get; set; }

    }
}
