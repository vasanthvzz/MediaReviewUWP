using MediaReviewClassLibrary.Models.Enitites;
using System.Threading.Tasks;

namespace MediaReviewClassLibrary.Data.DataHandler.Contract
{
    public interface IRatingDataHandler
    {
        Task<Rating> GetUserRating(long userId, long mediaId);
        Task<Rating> UpdateUserRating(Rating userRating);
    }
}
