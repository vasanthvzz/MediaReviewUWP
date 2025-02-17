using MediaReviewClassLibrary.Models;
using MediaReviewClassLibrary.Models.Enitites;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MediaReviewUWP.View.Contract
{
    public interface IManageReviewView
    {
        void UpdateMediaReviewList(List<MediaReviewBObj> mediaReviews);

        Task AddMediaReviewToList(MediaReviewBObj mediaReview);

        Task ChangeFolloweeStatus(long followeeId, bool isFollowing);

        Task DeleteReviewFromList(Review review);

        Task UpdateExistingReview(Review updatedReview);
    }
}