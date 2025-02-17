using MediaReviewClassLibrary.Models;
using System.Collections.Generic;

namespace MediaReviewUWP.ViewModel.Contract
{
    public interface ISearchPageViewModel
    {
        void UniversalSearch(string searchText);

        void SearchOnSuccess(List<MediaBObj> mediaList);
    }
}