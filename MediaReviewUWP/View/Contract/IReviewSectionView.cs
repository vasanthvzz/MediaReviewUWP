using MediaReviewClassLibrary.Models;
using MediaReviewClassLibrary.Models.Enitites;

namespace MediaReviewUWP.View.Contract
{
    public interface IReviewSectionView
    {
        void OnReviewAdded(MediaReviewBObj review);
    }
}
