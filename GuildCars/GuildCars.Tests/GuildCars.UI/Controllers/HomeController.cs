using GuildCars.Data;
using GuildCars.UI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GuildCars.UI.Controllers
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

       
    }
}