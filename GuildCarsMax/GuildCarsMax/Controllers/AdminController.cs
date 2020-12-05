using GuildCarsMax.Data;
using GuildCarsMax.Models;
using GuildCarsMax.Models.Queries;
using GuildCarsMax.Models.Tables;
using Microsoft.Ajax.Utilities;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations.Model;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Web;
using System.Web.Mvc;
using System.Web.Services.Description;

namespace GuildCarsMax.Controllers
{
    public class AdminController : Controller
    {
        [Authorize(Roles = "admin")]
        public ActionResult Vehicles()
        {
            return View();
        }

        [Authorize(Roles = "admin")]
        public ActionResult AddVehicle()
        {
            var model = new AddVehicleViewModel();
            var typesRepo = new TypesRepository();

            model.BodyStyles = new SelectList(typesRepo.GetAllBodyStyles(), "BodyStyleId", "BodyStyleName");
            model.ExteriorColors = new SelectList(typesRepo.GetAllExteriorColors(), "ExteriorColorId", "ExteriorColorName");
            model.InteriorColors = new SelectList(typesRepo.GetAllInteriorColors(), "InteriorColorId", "InteriorColorName");
            model.Makes = new SelectList(typesRepo.GetAllMakeTypes(), "MakeTypeId", "MakeTypeName");
            model.NewOrUsedTypes = new SelectList(typesRepo.GetNewOrUsedTypeOptions(), "NewOrUsedTypeId", "NewOrUsedTypeOption");
            model.TransmissionTypes = new SelectList(typesRepo.GetAllTransmissionTypes(), "TransmissionTypeId", "TransmissionTypeName");
            model.Vehicle = new Models.Tables.Vehicle();

            return View(model);

        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        public ActionResult AddVehicle(AddVehicleViewModel model)
        {
            if(ModelState.IsValid)
            {
                var repo = new VehicleInventoryRepository();

                try
                {
                    if (model.ImageUpload != null && model.ImageUpload.ContentLength > 0)
                    {
                        var savepath = Server.MapPath("~/Images");

                        string fileName = Path.GetFileNameWithoutExtension(model.ImageUpload.FileName);
                        string extension = Path.GetExtension(model.ImageUpload.FileName);

                        var filePath = Path.Combine(savepath, fileName + extension);

                        int counter = 1;
                        while (System.IO.File.Exists(filePath))
                        {
                            filePath = Path.Combine(savepath, fileName + counter.ToString() + extension);
                            counter++;
                        }

                        model.ImageUpload.SaveAs(filePath);
                        model.Vehicle.ImageFileName = Path.GetFileName(filePath);
                    }

                    repo.Insert(model.Vehicle);

                    return RedirectToAction("EditVehicle", new { id = model.Vehicle.VinNumber });
                }
                catch(Exception ex)
                {
                    throw ex;
                }
                }
            else
            {
                var typesRepo = new TypesRepository();

                model.BodyStyles = new SelectList(typesRepo.GetAllBodyStyles(), "BodyStyleId", "BodyStyleName");
                model.ExteriorColors = new SelectList(typesRepo.GetAllExteriorColors(), "ExteriorColorId", "ExteriorColorName");
                model.InteriorColors = new SelectList(typesRepo.GetAllInteriorColors(), "InteriorColorId", "InteriorColorName");
                model.Makes = new SelectList(typesRepo.GetAllMakeTypes(), "MakeTypeId", "MakeTypeName");
                model.NewOrUsedTypes = new SelectList(typesRepo.GetNewOrUsedTypeOptions(), "NewOrUsedTypeId", "NewOrUsedTypeOption");
                model.TransmissionTypes = new SelectList(typesRepo.GetAllTransmissionTypes(), "TransmissionTypeId", "TransmissionTypeName");

                return View(model);
            }
            
        }

        [Authorize(Roles = "admin")]
        public ActionResult EditVehicle(string vinNumber)
        {
            var model = new EditVehicleViewModel();

            var typesRepo = new TypesRepository();
            var vehicleRepo = new VehicleInventoryRepository();

            model.BodyStyles = new SelectList(typesRepo.GetAllBodyStyles(), "BodyStyleId", "BodyStyleName");
            model.ExteriorColors = new SelectList(typesRepo.GetAllExteriorColors(), "ExteriorColorId", "ExteriorColorName");
            model.InteriorColors = new SelectList(typesRepo.GetAllInteriorColors(), "InteriorColorId", "InteriorColorName");
            model.Makes = new SelectList(typesRepo.GetAllMakeTypes(), "MakeTypeId", "MakeTypeName");
            model.NewOrUsedTypes = new SelectList(typesRepo.GetNewOrUsedTypeOptions(), "NewOrUsedTypeId", "NewOrUsedTypeOption");
            model.TransmissionTypes = new SelectList(typesRepo.GetAllTransmissionTypes(), "TransmissionTypeId", "TransmissionTypeName");
            model.Vehicle = vehicleRepo.GetVehicle(vinNumber);

            return View(model);
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        public ActionResult EditVehicle(AddVehicleViewModel model)
        {
            if (ModelState.IsValid)
            {
                var repo = new VehicleInventoryRepository();

                try
                {
                    var oldVehicle = repo.GetVehicle(model.Vehicle.VinNumber);

                    if (model.ImageUpload != null && model.ImageUpload.ContentLength > 0)
                    {
                        var savepath = Server.MapPath("~/Images");

                        string fileName = Path.GetFileNameWithoutExtension(model.ImageUpload.FileName);
                        string extension = Path.GetExtension(model.ImageUpload.FileName);

                        var filePath = Path.Combine(savepath, fileName + extension);

                        int counter = 1;
                        while (System.IO.File.Exists(filePath))
                        {
                            filePath = Path.Combine(savepath, fileName + counter.ToString() + extension);
                            counter++;
                        }

                        model.ImageUpload.SaveAs(filePath);
                        model.Vehicle.ImageFileName = Path.GetFileName(filePath);

                        var oldPath = Path.Combine(savepath, oldVehicle.ImageFileName);
                        if (System.IO.File.Exists(oldPath))
                        {
                            System.IO.File.Delete(oldPath);
                        }
                    }
                    else
                    { 
                        model.Vehicle.ImageFileName = oldVehicle.ImageFileName;
                    }

                    repo.Update(model.Vehicle);

                    return RedirectToAction("Edit", new { id = model.Vehicle.VinNumber });
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            else
            {
                var typesRepo = new TypesRepository();

                model.BodyStyles = new SelectList(typesRepo.GetAllBodyStyles(), "BodyStyleId", "BodyStyleName");
                model.ExteriorColors = new SelectList(typesRepo.GetAllExteriorColors(), "ExteriorColorId", "ExteriorColorName");
                model.InteriorColors = new SelectList(typesRepo.GetAllInteriorColors(), "InteriorColorId", "InteriorColorName");
                model.Makes = new SelectList(typesRepo.GetAllMakeTypes(), "MakeTypeId", "MakeTypeName");
                model.NewOrUsedTypes = new SelectList(typesRepo.GetNewOrUsedTypeOptions(), "NewOrUsedTypeId", "NewOrUsedTypeOption");
                model.TransmissionTypes = new SelectList(typesRepo.GetAllTransmissionTypes(), "TransmissionTypeId", "TransmissionTypeName");

                return View(model);
            }

        }

        [Authorize(Roles = "admin")]
        public ActionResult Users()
        {
            using (var context = new ApplicationDbContext())
            {
                List<UserViewModel> userVM = new List<UserViewModel>();
                var userStore = new UserStore<ApplicationUser>(context);
                var userManager = new UserManager<ApplicationUser>(userStore);
                List<ApplicationUser> allUsers = userStore.Users.ToList();
                foreach (var user in allUsers)
                {
                    var row = new UserViewModel { UserId = user.Id, FirstName = user.FirstName, LastName = user.LastName, Email = user.Email, Role = userManager.GetRoles(user.Id).FirstOrDefault() };
                    userVM.Add(row);
                }
                return View(userVM);

            }

        }

        [Authorize(Roles = "admin")]
        public ActionResult AddMake()
        {
           var repo = new VehicleInventoryRepository();
           var model = new AddMakeViewModel();
           var makes = repo.GetMakeDetails();
           model.Makes = makes;

            return View(model);
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        public ActionResult AddMake(AddMakeViewModel model)
        {
            if (ModelState.IsValid)
            {
                var repo = new VehicleInventoryRepository();

                try
                {
                    repo.AddMake(model.MakeParameters);

                    return RedirectToAction("AddMake");
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            else
            {
                var repo = new VehicleInventoryRepository();

                var makes = repo.GetMakeDetails();
                model.Makes = makes;

                return View(model);
            }
        }

        [Authorize(Roles = "admin")]
        public ActionResult AddModel()
        {
            var repo = new VehicleInventoryRepository();
            var typesRepo = new TypesRepository();
            var model = new AddModelViewModel();
            model.Makes = new SelectList(typesRepo.GetAllMakeTypes(), "MakeTypeId", "MakeTypeName");
            model.Models = repo.GetModelDetails();

            return View(model);

        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        public ActionResult AddModel(AddModelViewModel model)
        {
            if (ModelState.IsValid)
            {
                var repo = new VehicleInventoryRepository();

                try
                {
                    repo.AddModel(model.ModelParameters);

                    return RedirectToAction("AddModel");
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

                model.Makes = new SelectList(typesRepo.GetAllMakeTypes(), "MakeTypeId", "MakeTypeName");
                model.Models = repo.GetModelDetails();

                return View(model);
            }
        }

        [Authorize(Roles = "admin")]
        public ActionResult Specials()
        {
            var repo = new VehicleInventoryRepository();          
            var model = new AddSpecialViewModel();
            model.Specials = repo.GetSpecials();

            return View(model);

        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        public ActionResult Specials(AddSpecialViewModel model)
        {
            if (ModelState.IsValid)
            {
                var repo = new VehicleInventoryRepository();

                try
                {
                    repo.AddSpecial(model.Special);

                    return RedirectToAction("Specials");
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            else
            {
                var repo = new VehicleInventoryRepository();
                model.Specials = repo.GetSpecials();

                return View(model);
            }
        }

        [Authorize(Roles = "admin")]
        public ActionResult Reports()
        {

            return View();

        }
        //.Select(u => new { UserId = u.Id, Name = u.FirstName + " " + u.LastName }).ToList()
        [Authorize(Roles = "admin")]
        public ActionResult SalesReport()
        {
            using (var context = new ApplicationDbContext())
            {
                var model = new SalesReportViewModel();

                var userStore = new UserStore<ApplicationUser>(context);
                var userManager = new UserManager<ApplicationUser>(userStore);

                var allUsers = userStore.Users.ToList();
                List<UserShortItem> salesUsers = new List<UserShortItem>();
                foreach(var user in allUsers)
                {
                    if (userManager.IsInRole(user.Id, "sales"))
                    {
                        UserShortItem salesUser = new UserShortItem();
                        salesUser.UserId = user.Id;
                        salesUser.Name = $"{user.FirstName} {user.LastName}";
                        salesUsers.Add(salesUser);
                        
                    }
                }
                model.FilterParameters = new SalesReportFilterParameters();
                model.Users = new SelectList(salesUsers, "UserId", "Name");

                return View(model);
            }

        }

        [Authorize(Roles = "admin")]
        public ActionResult InventoryReport()
        {
            var model = new InventoryReportViewModel();
            var repo = new VehicleInventoryRepository();
            model.NewVehicles = repo.NewVehicleInventoryReport();
            model.UsedVehicles = repo.UsedVehicleInventoryReport();

            return View(model);
        }

    }
}