using MediaReviewClassLibrary.Models.Enitites;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MediaReviewClassLibrary.Data.DataHandler.Contract
{
    public interface IRatingDataHandler
    {
        Task<Rating> GetUserRating(long userId, long mediaId);
        Task<Rating> UpdateUserRating(Rating userRating);
        Task<float> GetAverageRating(long mediaId);
        Task<long> GetRatedUserCount(long mediaId);
        Task<List<Rating>> GetAllUserRating(long userId);
    }
}
