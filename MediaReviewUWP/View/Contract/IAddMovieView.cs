using MediaReviewClassLibrary.Models.Enitites;
using System.Collections.Generic;

namespace MediaReviewUWP.View.Contract
{
    public interface IAddMovieView
    {
        void ShowMovieAdditionStatus(Media mediaDetail, bool success);

        void UpdateGenreList(List<Genre> genreList);
    }
}