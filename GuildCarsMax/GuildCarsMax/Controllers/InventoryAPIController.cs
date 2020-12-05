using GuildCarsMax.Data;
using GuildCarsMax.Models.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace GuildCarsMax.UI.Controllers
{
    public class InventoryAPIController : ApiController
    {
        [Route("api/Inventory/new/search")]
        [AcceptVerbs("POST")]
        public IHttpActionResult NewInventorySearch(VehicleInventorySearchParameters parameters)
        {
            var repo = new VehicleInventoryRepository();

            try
            {
                var results = repo.NewInventorySearch(parameters);
                return Ok(results);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("api/Inventory/used/search")]
        [AcceptVerbs("POST")]
        public IHttpActionResult UsedInventorySearch(VehicleInventorySearchParameters parameters)
        {
            var repo = new VehicleInventoryRepository();

            try
            {
                var results = repo.UsedInventorySearch(parameters);
                return Ok(results);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [Route("api/Inventory/sales/search")]
        [AcceptVerbs("POST")]
        public IHttpActionResult SalesInventorySearch(VehicleInventorySearchParameters parameters)
        {
            var repo = new VehicleInventoryRepository();

            try
            {
                var results = repo.SalesInventorySearch(parameters);
                return Ok(results);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("api/Inventory/getModels")]
        [AcceptVerbs("GET")]
        public IHttpActionResult GetMakes(int makeId)
        {
            var repo = new TypesRepository();

            try
            {
                var results = repo.GetAllModelTypesByMake(makeId);
                return Ok(results);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("api/Inventory/deleteVehicle")]
        [AcceptVerbs("DELETE")]
        public void DeleteVehicle(string vinNumber)
        {
            var repo = new VehicleInventoryRepository();

            repo.Delete(vinNumber);

        }

        [Route("api/Inventory/deleteSpecial")]
        [AcceptVerbs("DELETE")]
        public void DeleteVehicle(int specialId)
        {
            var repo = new VehicleInventoryRepository();

            repo.DeleteSpecial(specialId);

        }

        [Route("api/Inventory/sales/report")]
        [AcceptVerbs("POST")]
        public IHttpActionResult SalesReport(SalesReportFilterParameters parameters)
        {
            var repo = new SalesRepository();

            try
            {
                var results = repo.SalesReportSearch(parameters);
                return Ok(results);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
