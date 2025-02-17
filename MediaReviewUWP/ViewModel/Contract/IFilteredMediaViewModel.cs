using MediaReviewClassLibrary.Models;
using MediaReviewClassLibrary.Models.Enitites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaReviewUWP.ViewModel.Contract
{
    public interface IFilteredMediaViewModel
    {
        void GetFilteredMedia(List<Genre> genreList);
        void FilteredMediaFetched(List<MediaBObj> filteredMediaList);
    }
}
