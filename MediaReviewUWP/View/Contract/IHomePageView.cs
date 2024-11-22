using MediaReviewClassLibrary.Models.Enitites;
using System.Collections.Generic;

namespace MediaReviewUWP.View.Contract
{
    public interface IHomePageView : IView
    {
        void UpdateMediaList(List<Media> MediaList);
    }
}
