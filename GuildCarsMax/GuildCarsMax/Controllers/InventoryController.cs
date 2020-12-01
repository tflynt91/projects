using GuildCarsMax.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GuildCarsMax.UI.Controllers
{
    public class InventoryController : Controller
    {
        // GET: Inventory
        public ActionResult New()
        {
            return View();
        }

        public ActionResult Used()
        {
            return View();
        }

        public ActionResult Details(string vinNumber)
        {
            var repo = new VehicleInventoryRepository();
            var model = repo.GetVehicleDetails(vinNumber);

            return View(model);
        }
    }
}