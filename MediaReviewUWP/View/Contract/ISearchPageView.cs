using MediaReviewClassLibrary.Models;
using System.Collections.Generic;

namespace MediaReviewUWP.View.Contract
{
    public interface ISearchPageView
    {
        void UpdateSearch(List<MediaBObj> mediaList);
    }
}