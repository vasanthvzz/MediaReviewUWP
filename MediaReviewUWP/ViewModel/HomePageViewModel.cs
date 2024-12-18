using CommonClassLibrary;
using MediaReviewClassLibrary.Domain;
using MediaReviewUWP.View.Contract;
using MediaReviewUWP.ViewModel.Contract;
using System;
using System.Diagnostics;

namespace MediaReviewUWP.ViewModel
{
    public class HomePageViewModel : IHomePageViewModel
    {
        private IHomePageView _view;

        public HomePageViewModel(IHomePageView view)
        {
            _view = view;
        }

        public void GetAllMedia(long currentCount = 0,long requiredCount = 5)
        {
            GetAllMediaUseCase useCase = new GetAllMediaUseCase(new GetAllMediaRequest(currentCount,requiredCount), new GetAllMediaPresenterCallback(this));
            useCase.Execute();
        }

        public class GetAllMediaPresenterCallback : IGetAllMediaPresenterCallback
        {
            private HomePageViewModel _presenter;

            public GetAllMediaPresenterCallback(HomePageViewModel presenter)
            {
                _presenter = presenter;
            }

            public async void OnSuccess(ZResponse<GetAllMediaResponse> response)
            {
                await _presenter._view.Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
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
