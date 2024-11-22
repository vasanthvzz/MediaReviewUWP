using MediaReviewUWP.ViewObjects;
using System.Collections.Generic;

namespace MediaReviewUWP.View.Contract
{
    public interface IReviewListView
    {
        void UpdateMediaReviews(List<MediaReviewVObj> mediaReviews);
        void UpdateMediaReviews(MediaReviewVObj mediaReview);
    }
}
