using MediaReviewClassLibrary.Models;

namespace MediaReviewUWP.ViewModel.Contract
{
    public interface IShowMediaListViewModel
    {
        void GetMedia(long mediaId);
        void GetPresentMediaDetails();
        void UpdateMediaList(MediaDetailBObj mediaDetails);
    }
}