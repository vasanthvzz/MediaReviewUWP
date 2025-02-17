using CommonClassLibrary;
using MediaReviewClassLibrary.Domain;
using MediaReviewClassLibrary.Models.Enitites;
using MediaReviewUWP.View.Contract;
using MediaReviewUWP.ViewModel.Contract;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MediaReviewUWP.ViewModel
{
    public class HomePageViewModel : IHomePageViewModel
    {
        private IHomePageView _view;

        public HomePageViewModel(IHomePageView view)
        {
            _view = view;
        }

        public void GetAllGenre()
        {
            GetAllGenreUseCase uc = new GetAllGenreUseCase(new GetAllGenrePresenterCallback(this));
            uc.Execute();
        }

        public void GetAllMedia(long currentCount = 0, long requiredCount = 5)
        {
            GetAllMediaUseCase useCase = new GetAllMediaUseCase(new GetAllMediaRequest(currentCount, requiredCount), new GetAllMediaPresenterCallback(this));
            useCase.Execute();
        }

        public void GetTagSuccess(List<Genre> genreList)
        {
            _view.UpdateGenreList(genreList);
        }

        private class GetAllGenrePresenterCallback : IGetAllGenrePresenterCallback
        {
            private IHomePageViewModel _vm;

            public GetAllGenrePresenterCallback(IHomePageViewModel vm)
            {
                _vm = vm;
            }

            public void OnFailure(Exception exception)
            {
                Console.WriteLine(exception);
            }

            public void OnSuccess(ZResponse<GetAllGenreResponse> response)
            {
                _vm.GetTagSuccess(response?.Data?.GenreList);
            }
        }

        private class GetAllMediaPresenterCallback : IGetAllMediaPresenterCallback
        {
            private HomePageViewModel _presenter;

            public GetAllMediaPresenterCallback(HomePageViewModel presenter)
            {
                _presenter = presenter;
            }

            public void OnSuccess(ZResponse<GetAllMediaResponse> response)
            {
                _ =  _presenter._view.Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
                {
                    _presenter?._view?.UpdateMediaList(response.Data.MediaList);
                });
            }

            public void OnFailure(Exception exception)
            {
                Console.WriteLine(exception.ToString());
            }
        }
    }
}