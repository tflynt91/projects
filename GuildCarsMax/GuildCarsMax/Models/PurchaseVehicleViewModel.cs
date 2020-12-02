using GuildCarsMax.Models.Queries;
using GuildCarsMax.Models.Tables;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;

namespace GuildCarsMax.Models
{
    public class PurchaseVehicleViewModel : IValidatableObject
    {
        public Sale Sale { get; set; }

        [Required]
        public string Name { get; set; }
        public VehicleInventoryListingDetails Vehicle { get; set; }
        public IEnumerable<SelectListItem> PurchaseTypes { get; set; }
        public IEnumerable<SelectListItem> States { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            List<ValidationResult> errors = new List<ValidationResult>();

            if (String.IsNullOrWhiteSpace(Sale.Email) && String.IsNullOrWhiteSpace(Sale.Phone))
            {
                errors.Add(new ValidationResult("Please enter at least one of the following: Phone Number, Email"));
            }

            if(Sale.ZipCode.Length != 5)
            {
                errors.Add(new ValidationResult("Zip Code must be 5 digits"));
            }
            if(!IsEmailValid(Sale.Email))
            {
                errors.Add(new ValidationResult("Email Address is not in proper format"));
            }
            if(Sale.PurchasePrice < (Vehicle.SalePrice * (decimal).95))
            {
                errors.Add(new ValidationResult("Purchase Price cannot be less than 95% of the sales price"));
            }
            if(Sale.PurchasePrice > Vehicle.MSRP)
            {
                errors.Add(new ValidationResult("Purchase Price cannot exceed MSRP"));
            }

            return errors;
        }

        public bool IsEmailValid(string emailaddress)
        {
            try
            {
                MailAddress m = new MailAddress(emailaddress);
                return true;
            }
            catch (FormatException)
            {
                return false;
            }
        }
    }
}