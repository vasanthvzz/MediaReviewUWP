using MediaReviewClassLibrary.Models;

namespace MediaReviewUWP.ViewModel.Contract
{
    public interface IMediaPageViewModel
    {
        void GetMediaDetail(long movieId);

        void SendMediaDetail(MediaDetailBObj mediaDetail);
    }
}