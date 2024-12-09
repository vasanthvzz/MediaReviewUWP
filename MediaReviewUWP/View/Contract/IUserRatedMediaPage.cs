using MediaReviewClassLibrary.Models;
using MediaReviewClassLibrary.Models.Enitites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaReviewUWP.View.Contract
{
    public interface IUserRatedMediaPage
    {
        Task UpdateRatedMediaList(List<UserRatingBObj> userRatingList);
        Task UpdatedMediaRating(Rating rating);
    }
}
