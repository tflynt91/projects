using DvdLibrary.Models.Queries;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace DvdLibrary.Models.Repos
{
    public class DvdRepositoryEF : DbContext, IDvdRepository
    {
        public DvdRepositoryEF() 
            : base("DefaultConnection")
        {
        }


        public DbSet<DvdItem> DvdItems { get; set; }

        public void CreateDvd(string title, string releaseYear, string director, string rating, string notes)
        {
            DvdItem updatedItem = new DvdItem();
            updatedItem.DvdId = DvdItems.Max(d => d.DvdId) + 1;
            updatedItem.Title = title;
            updatedItem.Director = director;
            updatedItem.RatingType = rating;
            updatedItem.Notes = notes;
           
            DvdItems.Add(updatedItem);
            
        }

        public void DeleteDvd(int dvdId)
        {
            DvdItem deletedDvd = DvdItems.FirstOrDefault(d => d.DvdId == dvdId);

            DvdItems.Remove(deletedDvd);
        }

        public IEnumerable<DvdItem> GetDvdByDirectorName(string director)
        {
            IEnumerable<DvdItem> dvdsByDirector = DvdItems.Where(d => d.Director.Contains(director));

            return dvdsByDirector;
        }

        public DvdItem GetDvdById(int id)
        {

            DvdItem dvdById = DvdItems.Where(d => d.DvdId == id).FirstOrDefault();

            return dvdById;

        }

        public IEnumerable<DvdItem> GetDvdByRating(string rating)
        {
            IEnumerable<DvdItem> dvdsByRating = DvdItems.Where(d => d.RatingType == rating);

            return dvdsByRating;
        }

        public IEnumerable<DvdItem> GetDvdByReleaseYear(string year)
        {
            IEnumerable<DvdItem> dvdsByYear = DvdItems.Where(d => d.ReleaseYear.Contains(year));
            return dvdsByYear;
        }

        public IEnumerable<DvdItem> GetDvdByTitle(string title)
        {
            IEnumerable<DvdItem> dvdsByTitle = DvdItems.Where(d => d.Title.Contains(title));
            return dvdsByTitle;
        }

        public IEnumerable<DvdItem> GetDvds()
        {
            return DvdItems.Where(d => d.DvdId > 0);
        }

        public void UpdateDvd(string dvdId, string title, string releaseYear, string director, string rating, string notes)
        {
            DvdItem updatedItem = new DvdItem();
            updatedItem.DvdId = Int32.Parse(dvdId);
            updatedItem.Title = title;
            updatedItem.Director = director;
            updatedItem.RatingType = rating;
            updatedItem.ReleaseYear = releaseYear;
            updatedItem.Notes = notes;
            DvdItem dvdToUpdate = DvdItems.Where(d => d.DvdId == Int32.Parse(dvdId)).FirstOrDefault();
            DvdItems.Remove(dvdToUpdate);
            DvdItems.Add(updatedItem);
        }
    }
}