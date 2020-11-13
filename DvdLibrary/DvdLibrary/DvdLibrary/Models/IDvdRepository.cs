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
        void CreateDvd(string title, string releaseYear, string director, string rating, string notes);
        void UpdateDvd(string dvdId, string title, string releaseYear, string director, string rating, string notes);
        void DeleteDvd(int dvdId);



    }
}
