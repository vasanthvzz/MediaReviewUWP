using MediaReviewClassLibrary.Models.Enitites;
using System.Collections.Generic;

namespace MediaReviewUWP.ViewModel.Contract
{
    public interface IHomePageViewModel
    {
        void GetAllGenre();
        void GetAllMedia(long currentCount, long requiredCount);
        void GetTagSuccess(List<Genre> genreList);
    }
}