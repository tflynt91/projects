using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Exercises.Models.Data
{
    public class State : IValidatableObject
    {

        public string StateAbbreviation { get; set; }

        public string StateName { get; set; }

         public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            IEnumerable<string> stateAbbreviations = new List<string>() { "AL", "AK", "AZ", "AR", "CA", "CO", "CT", "DE", "FL", "GA", "HI", "ID", "IL", "IN", "IA", "KS", "KY", "LA", "ME", "MD", "MA", "MI", "MN", "MS", "MO", "MT", "NE", "NV", "NH", "NJ", "NM", "NY", "NC", "ND", "OH", "OK", "OR", "PA", "RI", "SC", "SD", "TN", "TX", "UT", "VT", "VA", "WA", "WV", "WI", "WY" };
            List<ValidationResult> errors = new List<ValidationResult>();
            if (string.IsNullOrEmpty(StateAbbreviation))
            {
                errors.Add(new ValidationResult("State abbreviation is required", new[] { "StateAbbreviation" }));
            }
            else if (!stateAbbreviations.Contains(StateAbbreviation.ToUpper()))
            {
                errors.Add(new ValidationResult("Please enter valid State Abbreviation(i.e NY", new[] { "StateAbbreviation" }));
            }
       
            return errors;
        }
    }
}