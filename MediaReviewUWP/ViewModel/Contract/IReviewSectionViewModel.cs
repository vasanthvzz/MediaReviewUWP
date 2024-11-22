using MediaReviewClassLibrary.Models;
using MediaReviewClassLibrary.Models.Enitites;

namespace MediaReviewUWP.ViewModel.Contract
{
    public interface IReviewSectionViewModel
    {
        void AddReview(long mediaId, string description);
        void SendReviewUpdate(MediaReviewBObj review);
    }
}
