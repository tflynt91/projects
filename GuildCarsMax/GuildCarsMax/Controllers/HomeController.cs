using GuildCarsMax.Data;
using GuildCarsMax.Models.Tables;
using GuildCarsMax.UI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Results;
using System.Web.Mvc;

namespace GuildCarsMax.UI.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var repo = new VehicleInventoryRepository();
            HomeViewModel homeView = new HomeViewModel();
            homeView.Specials = repo.GetSpecials();
            homeView.FeaturedVehicles = repo.GetFeaturedVehicles();
            return View(homeView);
        }

        public ActionResult Specials()
        {
            var repo = new VehicleInventoryRepository();
            IEnumerable<Special> specials = repo.GetSpecials();
            return View(specials);
        }

        public ActionResult AddContact(string message)
        {
            var model = new Contact();

            if(!String.IsNullOrEmpty(message))
            {
                model.Message = message;
            }

            return View(model);

        }

        [HttpPost]
        public ActionResult AddContact(Contact contact)
        {
            if(ModelState.IsValid)
            {
                var repo = new SalesRepository();

                try
                {
                    repo.ContactInsert(contact);

                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            else
            {
                var model = contact;

                return View(model);
            }
        }
       
    }
}