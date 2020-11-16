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

        public void CreateDvd(DvdItem dvdItem)
        {
            DvdItem updatedItem = new DvdItem();
            updatedItem.DvdId = DvdItems.Max(d => d.DvdId) + 1;
            updatedItem.Title = dvdItem.Title;
            updatedItem.ReleaseYear = dvdItem.ReleaseYear;
            updatedItem.Director = dvdItem.Director;
            updatedItem.RatingType = dvdItem.RatingType;
            updatedItem.Notes = dvdItem.Notes;
           
            DvdItems.Add(updatedItem);
            SaveChanges();
            
        }

        public void DeleteDvd(int dvdId)
        {
            DvdItem deletedDvd = DvdItems.FirstOrDefault(d => d.DvdId == dvdId);

            DvdItems.Remove(deletedDvd);
            SaveChanges();
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
            return DvdItems;
        }

        public void UpdateDvd(DvdItem dvdItem)
        {
            DvdItem updatedItem = new DvdItem();
            updatedItem.DvdId = dvdItem.DvdId;
            updatedItem.Title = dvdItem.Title;
            updatedItem.Director = dvdItem.Director;
            updatedItem.RatingType = dvdItem.RatingType;
            updatedItem.ReleaseYear = dvdItem.ReleaseYear;
            updatedItem.Notes = dvdItem.Notes;
            DvdItem dvdToUpdate = DvdItems.Where(d => d.DvdId == dvdItem.DvdId).FirstOrDefault();
            DvdItems.Remove(dvdToUpdate);
            DvdItems.Add(updatedItem);
            SaveChanges();
        }
    }
}