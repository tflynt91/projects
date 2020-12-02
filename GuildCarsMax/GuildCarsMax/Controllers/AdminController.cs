using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GuildCarsMax.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult Vehicles()
        {
            return View();
        }

        public ActionResult AddVehicle()
        {

        }
    }
}