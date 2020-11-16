using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DvdLibrary.Models.Queries
{
    public class DvdItem
    {
        [Key]
        public int? DvdId { get; set; }
        public string Title { get; set; }
        public string ReleaseYear { get; set; }
        public string Director { get; set; }
        public string RatingType { get; set; }
        public string Notes { get; set; }

    }
}