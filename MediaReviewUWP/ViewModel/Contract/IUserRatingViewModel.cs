using MediaReviewClassLibrary.Models.Enitites;

namespace MediaReviewUWP.ViewModel.Contract
{
    public interface IUserRatingViewModel
    {
        void UpdateUserRating(Rating userRating);

        void SendUserRating(Rating userRating);
    }
}