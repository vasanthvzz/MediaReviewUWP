using MediaReviewClassLibrary.Data.Contract;
using MediaReviewClassLibrary.Data.DataHandler.Contract;
using MediaReviewClassLibrary.Models.Enitites;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MediaReviewClassLibrary.Data.DataHandler
{
    public class GenreDataHandler : IGenreDataHandler
    {
        private IDatabaseAdapter _databaseAdapter = MediaReviewDIServiceProvider.GetRequiredService<IDatabaseAdapter>();

        public async Task<Genre> GetGenreById(long id)
        {
            return await _databaseAdapter.FindAsync<Genre>(id);
        }

        public async Task<List<Genre>> GetAllGenre()
        {
            return await _databaseAdapter.GetTableAsync<Genre>();
        }

        public async Task<List<Genre>> GetGenreByMediaId(long mediaId)
        {
            var customQuery = "SELECT genre_id FROM genre_mapper WHERE media_id = ?";
            var result = await _databaseAdapter.ExecuteQuery<GenreMapper>(customQuery, mediaId);
            List<Genre> resultList = new List<Genre>();
            foreach (var item in result)
            {
                resultList.Add(await GetGenreById(item.GenreId));
            }
            return resultList;
        }

        public async Task<List<long>> GetMediaIdsByGenreId(long genreId)
        {
            var customQuery = "SELECT media_id FROM genre_mapper WHERE genre_id = ?";
            var result = await _databaseAdapter.ExecuteQuery<GenreMapper>(customQuery, genreId);
            return result?.Select(gm => gm.MediaId).ToList();
        }

        public void AddGenre(GenreMapper genre)
        {
            _databaseAdapter.InsertOrReplaceAsync<GenreMapper>(genre);
        }
    }
}