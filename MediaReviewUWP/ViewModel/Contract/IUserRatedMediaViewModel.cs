using MediaReviewClassLibrary.Models;
using MediaReviewClassLibrary.Models.Enitites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
