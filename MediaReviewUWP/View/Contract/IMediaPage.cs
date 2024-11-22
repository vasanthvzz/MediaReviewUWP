using MediaReviewClassLibrary.Models;

namespace MediaReviewUWP.View.Contract
{
    public interface IMediaPage : IView
    {
        void UpdateMediaPage(MediaDetailBObj mediaDetailBObj);
    }
}
