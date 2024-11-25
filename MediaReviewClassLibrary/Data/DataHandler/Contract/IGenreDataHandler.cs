using MediaReviewClassLibrary.Models.Enitites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaReviewClassLibrary.Data.DataHandler.Contract
{
    public interface IGenreDataHandler
    {
        Task<List<Genre>> GetGenreByMediaId(long mediaId);
        Genre GetGenreById(long id);
        List<long> GetMediaIdsByGenreId(long gereId);   
    }
}
