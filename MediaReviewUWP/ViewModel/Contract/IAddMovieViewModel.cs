using MediaReviewClassLibrary.Domain;
using MediaReviewClassLibrary.Models;
using MediaReviewClassLibrary.Models.Enitites;
using System.Collections.Generic;

namespace MediaReviewUWP.ViewModel.Contract
{
    public interface IAddMovieViewModel
    {
        void AddMovie(AddMediaBObj media);

        void GetAllGenre();

        void OnSuccess(AddMediaResponse data);

        void SendGenreList(List<Genre> genreList);
    }
}