﻿using GuildCarsMax.Models.Tables;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GuildCarsMax.Models
{
    public class AddVehicleViewModel : IValidatableObject
    {
        public Vehicle Vehicle { get; set; }
        public HttpPostedFileBase ImageUpload { get; set; }
        public IEnumerable<SelectListItem> Makes { get; set; }
        public IEnumerable<SelectListItem> Models { get; set; }
        public IEnumerable<SelectListItem> NewOrUsedTypes { get; set; }
        public IEnumerable<SelectListItem> BodyStyles { get; set; }
        public IEnumerable<SelectListItem> TransmissionTypes { get; set; }
        public IEnumerable<SelectListItem> ExteriorColors { get; set; }
        public IEnumerable<SelectListItem> InteriorColors { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            List<ValidationResult> errors = new List<ValidationResult>();

            if (!(Vehicle.Year > 2000 && Vehicle.Year < (int)DateTime.Now.Year + 1))
            {
                errors.Add(new ValidationResult($"Year must be between 2000 and {DateTime.Now.AddYears(1).Year}"));
            }

            if(string.IsNullOrEmpty(Vehicle.VinNumber))
            {
                errors.Add(new ValidationResult("Vin Number is required."));
            }
            if(string.IsNullOrEmpty(Vehicle.VehicleDescription))
            {
                errors.Add(new ValidationResult("Vehicle Description is required."));
            }

            if(Vehicle.NewOrUsedTypeId == 1)
            {
                if(Vehicle.Mileage < 0)
                {
                    errors.Add(new ValidationResult("Mileage should be above 0"));
                }
                else if(Vehicle.Mileage > 1000)
                {
                    errors.Add(new ValidationResult("Mileage on new cars cannot exceed 1000"));
                }
            }
            else if(Vehicle.NewOrUsedTypeId == 2)
            {
                if(Vehicle.Mileage < 1000)
                {
                    errors.Add(new ValidationResult("Mileage on used cars must be greater than 1000"));
                }
            }

            if(Vehicle.MSRP < 0 || Vehicle.SalePrice < 0)
            {
                errors.Add(new ValidationResult("MSRP and Sale Price must be a positive number"));
            }
            if(Vehicle.SalePrice > Vehicle.MSRP)
            {
                errors.Add(new ValidationResult("Sale price must not be greater than MSRP"));
            }
            if (ImageUpload != null && ImageUpload.ContentLength > 0)
            {
                var extensions = new string[] { ".jpg", ".png", ".gif", ".jpeg" };

                var extension = Path.GetExtension(ImageUpload.FileName);

                if(!extensions.Contains(extension))
                {
                    errors.Add(new ValidationResult("Image file musgt be a jpg, png, gif, or jpeg"));
                }
            }
            else
            {
                errors.Add(new ValidationResult("Image file is required"));
            }

            return errors;
        }
    }
}