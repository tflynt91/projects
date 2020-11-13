using DvdLibrary.Models;
using DvdLibrary.Models.Queries;
using DvdLibrary.Repos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace DvdLibrary.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class DvdLibraryController : ApiController
    {
        private IDvdRepository _dvdRepository = DvdRepositoryFactory.GetRepository();
        

        [Route("dvds")]
        [AcceptVerbs("GET")]
        public IHttpActionResult GetDvds()
        {
            return Ok(_dvdRepository.GetDvds());
        }

        [Route("dvd/{id}")]
        [AcceptVerbs("GET")]
        public IHttpActionResult GetDvdById(int id)
        {
            DvdItem found = _dvdRepository.GetDvdById(id);

            if (found == null)
                return NotFound();

            return Ok(found);
        }

        [Route("dvds/title/{title}")]
        [AcceptVerbs("GET")]
        public IHttpActionResult GetDvdByTitle(string title)
        {
            IEnumerable<DvdItem> found = _dvdRepository.GetDvdByTitle(title);

            if (found == null)
                return NotFound();

            return Ok(found);
        }

        [Route("dvds/year/{releaseYear}")]
        [AcceptVerbs("GET")]
        public IHttpActionResult GetDvdByReleaseYear(string releaseYear)
        {
            IEnumerable<DvdItem> found = _dvdRepository.GetDvdByReleaseYear(releaseYear);

            if (found == null)
                return NotFound();

            return Ok(found);
        }

        [Route("dvds/director/{directorName}")]
        [AcceptVerbs("GET")]
        public IHttpActionResult GetDvdByDirector(string director)
        {
            IEnumerable<DvdItem> found = _dvdRepository.GetDvdByDirectorName(director);

            if (found == null)
                return NotFound();

            return Ok(found);
        }

        [Route("dvds/rating/{rating}")]
        [AcceptVerbs("GET")]
        public IHttpActionResult GetDvdByRating(string rating)
        {
            IEnumerable<DvdItem> found = _dvdRepository.GetDvdByRating(rating);

            if (found == null)
                return NotFound();

            return Ok(found);
        }

        [Route("dvd/")]
        [AcceptVerbs("POST")]
        public IHttpActionResult CreateDvd(string title, string releaseYear, string director, string ratingType, string notes)
        {
            _dvdRepository.CreateDvd(title, releaseYear, director, ratingType, notes);

            return Ok();
        }

        [Route("dvd/{id}")]
        [AcceptVerbs("PUT")]
        public void UpdateDvd(string dvdId, string title, string releaseYear, string director, string ratingType, string notes)
        {
            _dvdRepository.UpdateDvd(dvdId, title, releaseYear, director, ratingType, notes);
        }

        [Route("dvd/{id}")]
        [AcceptVerbs("DELETE")]
        public void DeleteDvd(int id)
        {
            _dvdRepository.DeleteDvd(id);
        }
    }
}

