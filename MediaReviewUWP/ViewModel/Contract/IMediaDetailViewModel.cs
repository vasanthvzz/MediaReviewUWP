namespace MediaReviewUWP.ViewModel.Contract
{
    public interface IMediaDetailViewModel
    {
        void GetMediaRating(long mediaId);

        void SendUpdatedMediaRating(float MediaRating);
    }
}