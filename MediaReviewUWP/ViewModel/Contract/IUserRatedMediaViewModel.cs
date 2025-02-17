using MediaReviewClassLibrary.Models;
using MediaReviewClassLibrary.Models.Enitites;
using System.Collections.Generic;

namespace MediaReviewUWP.ViewModel.Contract
{
    public interface IUserRatedMediaViewModel
    {
        void SendRatedMedia(List<UserRatingBObj> ratedMedia);

        void GetUserRatedMedia();

        void ChangeUserRating(long mediaId, short userRating);

        void SendUpdatedRating(Rating rating);
    }
}