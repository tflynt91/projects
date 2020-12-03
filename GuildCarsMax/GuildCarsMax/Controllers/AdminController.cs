using GuildCarsMax.Data;
using GuildCarsMax.Models;
using GuildCarsMax.Models.Tables;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Services.Description;

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

        
    }
}