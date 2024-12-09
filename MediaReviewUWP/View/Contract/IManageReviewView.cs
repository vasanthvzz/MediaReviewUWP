using MediaReviewClassLibrary.Models.Enitites;
using MediaReviewClassLibrary.Models;
using MediaReviewUWP.ViewObject;
using System.Collections.Generic;

namespace MediaReviewUWP.View.Contract
{
    public interface IManageReviewView
    {
        void UpdateMediaReviewList(List<MediaReviewBObj> mediaReviews);
        void AddMediaReviewToList(MediaReviewBObj mediaReview);
        void ChangeFolloweeStatus(long followeeId, bool isFollowing);
        void DeleteReviewFromList(Review review);
        void UpdateExistingReview(Review updatedReview);
    }
}
