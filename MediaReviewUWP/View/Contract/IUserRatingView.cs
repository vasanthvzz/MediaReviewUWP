using MediaReviewClassLibrary.Models.Enitites;
using System.Threading.Tasks;

namespace MediaReviewUWP.View.Contract
{
    public interface IUserRatingView : IView
    {
        Task UpdatedUserRating(Rating userRating);
    }
}