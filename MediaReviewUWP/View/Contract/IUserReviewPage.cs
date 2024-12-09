using MediaReviewClassLibrary.Models;
using MediaReviewClassLibrary.Models.Enitites;
using System.Collections.Generic;

namespace MediaReviewUWP.View.Contract
{
    public interface IUserReviewPage
    {
        void DeleteReview(Review deletedReview);
        void UpdateExistingReview(Review updatedReview);
        void UpdateUserReviews(List<UserReviewBObj> userReviews);
    }
}
