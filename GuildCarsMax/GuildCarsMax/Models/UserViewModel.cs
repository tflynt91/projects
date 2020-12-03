using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GuildCarsMax.Models
{
    public class UserViewModel
    {
        public IEnumerable<ApplicationUser> Users { get; set; }
    }
}