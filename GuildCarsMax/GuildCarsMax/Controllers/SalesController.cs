using GuildCarsMax.Data;
using GuildCarsMax.Models;
using GuildGuildCarsMaxCars.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace GuildCarsMax.Controllers
{
    public class SalesController : Controller
    {
        [Authorize(Roles="admin,sales")]
        // GET: Sales
        public ActionResult Index()
        {
            return View();
        }

        [Authorize(Roles = "admin,sales")]
        public ActionResult Purchase(string vinNumber)
        {
            var repo = new VehicleInventoryRepository();
            var typesRepo = new TypesRepository();
            var statesRepo = new StatesRepository();
            var model = new PurchaseVehicleViewModel();
            model.Vehicle = repo.GetVehicleDetails(vinNumber);
            model.PurchaseTypes = typesRepo.GetAllPurchaseTypes();
            model.States = statesRepo.GetAll();


            return View(model);
        }
    }
}