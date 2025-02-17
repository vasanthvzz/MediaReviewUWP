using CommonClassLibrary;
using MediaReviewClassLibrary.Domain;
using MediaReviewClassLibrary.Models;
using MediaReviewClassLibrary.Models.Enitites;
using MediaReviewUWP.View.Contract;
using MediaReviewUWP.ViewModel.Contract;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace MediaReviewUWP.ViewModel
{
    public class AddMovieViewModel : IAddMovieViewModel
    {
        private IAddMovieView _view;

        public AddMovieViewModel(IAddMovieView view)
        {
            _view = view;
        }

        public void AddMovie(AddMediaBObj media)
        {
            AddMediaPresenterCallback callback = new AddMediaPresenterCallback(this);
            AddMediaRequest request = new AddMediaRequest(media);
            AddMediaUseCase uc = new AddMediaUseCase(request, callback);
            uc.Execute();
        }

        public void GetAllGenre()
        {
            GetAllGenrePresenterCallback callback = new GetAllGenrePresenterCallback(this);
            GetAllGenreUseCase uc = new GetAllGenreUseCase(callback);
            uc.Execute();
        }

        public void OnSuccess(AddMediaResponse data)
        {
            _view.ShowMovieAdditionStatus(data.MediaDetail, data.Success);
        }

        public void SendGenreList(List<Genre> genreList)
        {
            _view.UpdateGenreList(genreList);
        }
    }

    public class AddMediaPresenterCallback : IAddMediaPresenterCallback
    {
        private IAddMovieViewModel _vm;

        public AddMediaPresenterCallback(IAddMovieViewModel vm)
        {
            _vm = vm;
        }

        public void OnFailure(Exception exception)
        {
            Debug.WriteLine(exception);
        }

        public void OnSuccess(ZResponse<AddMediaResponse> response)
        {
            _vm?.OnSuccess(response?.Data);
        }
    }

    public class GetAllGenrePresenterCallback : IGetAllGenrePresenterCallback
    {
        private IAddMovieViewModel _vm;

        public GetAllGenrePresenterCallback(IAddMovieViewModel vm)
        {
            _vm = vm;
        }

        public void OnFailure(Exception exception)
        {
            Debug.WriteLine(exception);
        }

        public void OnSuccess(ZResponse<GetAllGenreResponse> response)
        {
            _vm?.SendGenreList(response?.Data?.GenreList);
        }
    }
}