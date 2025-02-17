using MediaReviewClassLibrary.Models;
using MediaReviewClassLibrary.Models.Enitites;
using System.Collections.Generic;

namespace MediaReviewUWP.ViewModel.Contract
{
    public interface IUserReviewViewModel
    {
        void DeleteReview(long reviewId);

        void EditReview(long reviewId, string reviewContent);

        void GetUserReviews();

        void SendDeletedReview(Review deletedReview);

        void SendEditedReview(Review updatedReview);

        void UpdateUserReview(List<UserReviewBObj> userReviews);
    }
}