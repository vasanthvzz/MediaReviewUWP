using MediaReviewClassLibrary.Data.Contract;
using MediaReviewClassLibrary.Data.DataHandler.Contract;
using MediaReviewClassLibrary.Models.Enitites;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;

namespace MediaReviewClassLibrary.Data.DataHandler
{
    public class GenreDataHandler : IGenreDataHandler
    {
        private IDatabaseAdapter _databaseAdapter = MediaReviewDIServiceProvider.GetServiceProvider().GetRequiredService<IDatabaseAdapter>();

        public Genre GetGenreById(long id)
        {
            return _databaseAdapter.FindAsync<Genre>(id).Result;
        }

        public async Task<List<Genre>> GetGenreByMediaId(long mediaId)
        {
            var customQuery = "SELECT genre_id FROM genre_mapper WHERE media_id = ?";
            var result = await _databaseAdapter.ExecuteQuery<GenreMapper>(customQuery, mediaId);
            List<Genre> resultList = new List<Genre>();
            foreach (var item in result)
            {
               resultList.Add(GetGenreById(item.GenreId));
            }
            return resultList;
        }

        public List<long> GetMediaIdsByGenreId(long gereId)
        {
            return null;
        }
    }
}
