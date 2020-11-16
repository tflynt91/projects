using DvdLibrary.Models.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DvdLibrary.Models
{
    public interface IDvdRepository
    {
        IEnumerable<DvdItem> GetDvds();
        DvdItem GetDvdById(int id);
        IEnumerable<DvdItem> GetDvdByTitle(string title);
        IEnumerable<DvdItem> GetDvdByReleaseYear(string year);
        IEnumerable<DvdItem> GetDvdByDirectorName(string director);
        IEnumerable<DvdItem> GetDvdByRating(string rating);
        void CreateDvd(DvdItem dvdItem);
        void UpdateDvd(DvdItem dvdItem);
        void DeleteDvd(int dvdId);



    }
}
