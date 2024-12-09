using MediaReviewClassLibrary.Domain;
using MediaReviewClassLibrary.Models;
using MediaReviewClassLibrary.Models.Enitites;
using System.Collections.Generic;

namespace MediaReviewUWP.ViewModel.Contract
{
    public interface IReviewSectionViewModel
    {
        void GetMediaReviews(long mediaId);
        void AddReview(long mediaId, string description);
        void EditReview(long reviewId, long userId, string reviewContent);
        void DeleteReviewInMediaList(Review review);
        void UpdateFollow(long followeeId, bool isFollowing);
        void SendEditedReview(Review updatedReview);
        void UpdateMediaReview(List<MediaReviewBObj> mediaReviews);
        void UpdateMediaReview(MediaReviewBObj mediaReview);
        void DeleteReview(long reviewId);
        void OnFollowUpdate(long followeeId, bool isFollowing);
    }
}
