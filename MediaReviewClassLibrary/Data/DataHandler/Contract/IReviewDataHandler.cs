using MediaReviewClassLibrary.Models.Enitites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaReviewClassLibrary.Data.DataHandler.Contract
{
    public interface IReviewDataHandler
    {
        void AddReview(Review review);
        Task<List<Review>> GetReviewsByMedia(long mediaId);
        Task<Review> GetReviewById(long id);
        Task<Review> UpdateReview(long reviewId, string reviewContent);
        Task DeleteReview(long reviewId);
        Task<List<Review>> GetAllUserReviews(long userId);
    }
}
