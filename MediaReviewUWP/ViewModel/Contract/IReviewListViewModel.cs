using MediaReviewClassLibrary.Models;
using System.Collections.Generic;

namespace MediaReviewUWP.ViewModel.Contract
{
    public interface IReviewListViewModel
    {
        void GetMediaReviews(long mediaId);
        void SendMediaReviews(List<MediaReviewBObj> mediaReviews);
    }
}
