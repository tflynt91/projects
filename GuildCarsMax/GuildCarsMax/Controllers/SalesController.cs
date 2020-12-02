using GuildCarsMax.Data;
using GuildCarsMax.Models;
using GuildCarsMax.Models.Tables;
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
        [Authorize(Roles = "admin,sales")]
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
            model.PurchaseTypes = new SelectList(typesRepo.GetAllPurchaseTypes(), "PurchaseTypeId", "PurchaseTypeName");
            model.States = new SelectList(statesRepo.GetAll(), "StateId", "StateName");


            return View(model);
        }

        [Authorize(Roles = "admin,sales")]
        [HttpPost]
        public ActionResult Purchase(PurchaseVehicleViewModel model)
        {
            if (ModelState.IsValid)
            {
                var repo = new SalesRepository();
                var vehicleRepo = new VehicleInventoryRepository();
                var sale = new Sale();


                try
                {
                    sale = model.Sale;

                    if(model.Name.Contains(" "))
                    {
                        string[] name = model.Name.Split(' ');
                        sale.FirstName = name[0];
                        sale.LastName = name[1];
                    }
                    else
                    {
                        sale.FirstName = model.Name;
                    }

                    repo.Insert(sale);
                    var updatedVehicle = vehicleRepo.GetVehicle(model.Sale.VinNumber);
                    updatedVehicle.Sold = true;
                    vehicleRepo.Update(updatedVehicle);
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            else
            {
                var repo = new VehicleInventoryRepository();
                var typesRepo = new TypesRepository();
                var statesRepo = new StatesRepository();

                PurchaseVehicleViewModel newModel = new PurchaseVehicleViewModel
                {
                    Name = model.Name,
                    PurchaseTypes = new SelectList(typesRepo.GetAllPurchaseTypes(), "PurchaseTypeId", "PurchaseTypeName"),
                    States = new SelectList(statesRepo.GetAll(), "StateId", "StateName"),
                    Vehicle = repo.GetVehicleDetails(model.Sale.VinNumber),
                    Sale = model.Sale
                };

                return View(newModel);
            }
        }
    }
}