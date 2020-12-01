using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace GuildCarsMax.Models.Tables
{
    public class Contact : IValidatableObject
    {
        public int? ContactId { get; set; }

        [Required]
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }

        [Required]
        public string Message { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            List<ValidationResult> errors = new List<ValidationResult>();

            if(String.IsNullOrWhiteSpace(Email) && String.IsNullOrWhiteSpace(Phone))
            {
                errors.Add(new ValidationResult("Please enter at least one of the following: Phone Number, Email"));
            }

            return errors;
        }
    }
}
