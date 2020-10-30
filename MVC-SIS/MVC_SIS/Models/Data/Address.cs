using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Exercises.Models.Data
{
    public class Address
    {
        public int AddressId { get; set; }

        [Required(ErrorMessage = "Street Address is required.")]
        public string Street1 { get; set; }
        public string Street2 { get; set; }

        [Required(ErrorMessage = "City name is required.")]
        public string City { get; set; }

        public State State { get; set; }

        [Required(ErrorMessage = "Zip Code is required.")]
        [StringLength(5, ErrorMessage = "Zip Code must be 5 digits.")]
        public string PostalCode { get; set; }
    }
}