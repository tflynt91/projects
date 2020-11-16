using DvdLibrary.Models;
using DvdLibrary.Models.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DvdLibrary.Repos
{
    public class DvdRepositoryMock : IDvdRepository
    {
       
            private static List<DvdItem> _dvds;

            static DvdRepositoryMock()
            {
                    _dvds = new List<DvdItem>()
                {
                    new DvdItem { DvdId = 1, Title="Die Hard", ReleaseYear="1988", Director="John McTiernan", RatingType="R", Notes="A Great Action Flick!" },
                    new DvdItem { DvdId = 2, Title="Total Recall", ReleaseYear="1990", Director="Paul Verhoeven", RatingType="R", Notes="A Great Sci-Fi Flick!" },
                    new DvdItem { DvdId = 3, Title="Star Wars", ReleaseYear="1977", Director="George Lucas", RatingType="PG", Notes="A Classic Blockbuster!" }
                };
            }

        public IEnumerable<DvdItem> GetDvds()
        {
            return _dvds;
        }

        public DvdItem GetDvdById(int id)
        {
            return _dvds.FirstOrDefault(d => d.DvdId == id);
        }

        public IEnumerable<DvdItem> GetDvdByTitle(string title)
        {
            return _dvds.FindAll(d => d.Title == title);
        }

        public IEnumerable<DvdItem> GetDvdByReleaseYear(string year)
        {
            return _dvds.FindAll(d => d.ReleaseYear == year);
        }

        public IEnumerable<DvdItem> GetDvdByDirectorName(string director)
        {
            return _dvds.FindAll(d => d.Director == director);
        }

        public IEnumerable<DvdItem> GetDvdByRating(string rating)
        {
            return _dvds.FindAll(d => d.RatingType == rating);
        }

        public void CreateDvd(DvdItem dvdItem)
        {
            DvdItem newDvd = new DvdItem();

            if (_dvds.Any())
            {
                newDvd.DvdId = _dvds.Max(d => d.DvdId) + 1;
            }
            else
            {
                newDvd.DvdId = 0;
            }

            newDvd.Title = dvdItem.Title;
            newDvd.ReleaseYear = dvdItem.ReleaseYear;
            newDvd.Director = dvdItem.Director;
            newDvd.RatingType = dvdItem.RatingType;
            newDvd.Notes = dvdItem.Notes;

            _dvds.Add(newDvd);
            
        }

        public void UpdateDvd(DvdItem dvdItem)
        {
            DvdItem updatedDvd = new DvdItem();
            _dvds.RemoveAll(d => d.DvdId == dvdItem.DvdId);
            updatedDvd.Title = dvdItem.Title;
            updatedDvd.ReleaseYear = dvdItem.ReleaseYear;
            updatedDvd.Director = dvdItem.Director;
            updatedDvd.RatingType = dvdItem.RatingType;
            updatedDvd.Notes = dvdItem.Notes;

            _dvds.Add(updatedDvd);
        }

        public void DeleteDvd(int dvdId)
        {
            _dvds.RemoveAll(d => d.DvdId == dvdId);
        }

    }
}