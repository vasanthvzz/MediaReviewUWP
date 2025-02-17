using MediaReviewClassLibrary.Models.Enitites;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MediaReviewClassLibrary.Data.DataHandler.Contract
{
    public interface IGenreDataHandler
    {
        Task<List<Genre>> GetGenreByMediaId(long mediaId);

        Task<Genre> GetGenreById(long id);

        Task<List<long>> GetMediaIdsByGenreId(long genreId);

        Task<List<Genre>> GetAllGenre();

        void AddGenre(GenreMapper genre);
    }
}