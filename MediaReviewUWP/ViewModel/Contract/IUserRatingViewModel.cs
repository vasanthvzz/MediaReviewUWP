using MediaReviewClassLibrary.Models.Enitites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaReviewUWP.ViewModel.Contract
{
    public interface IUserRatingViewModel
    {
        void UpdateUserRating(Rating userRating);
        void SendUserRating(Rating userRating);
    }
}
