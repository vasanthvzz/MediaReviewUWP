using MediaReviewClassLibrary.Models;
using MediaReviewClassLibrary.Models.Enitites;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MediaReviewUWP.View.Contract
{
    public interface IUserReviewPage
    {
        Task DeleteReview(Review deletedReview);

        Task UpdateExistingReview(Review updatedReview);

        Task UpdateUserReviews(List<UserReviewBObj> userReviews);
    }
}