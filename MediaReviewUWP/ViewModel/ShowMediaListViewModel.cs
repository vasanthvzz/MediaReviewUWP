using CommonClassLibrary;
using MediaReviewClassLibrary.Data;
using MediaReviewClassLibrary.Domain;
using MediaReviewClassLibrary.Models;
using MediaReviewUWP.View.Contract;
using MediaReviewUWP.ViewModel.Contract;
using System;
using System.Diagnostics;

namespace MediaReviewUWP.ViewModel
{
    public class ShowMediaListViewModel : IShowMediaListViewModel
    {
        private IShowMediaListView _view;

        public ShowMediaListViewModel(IShowMediaListView view)
        {
            _view = view;
        }

        public void GetMedia(long mediaId)
        {
            GetMediaDetailRequest request = new GetMediaDetailRequest(SessionManager.User.UserId, mediaId);
            GetMediaDetailPresenterCallback callback = new GetMediaDetailPresenterCallback(this);
            GetMediaDetailUseCase uc = new GetMediaDetailUseCase(request, callback);
            uc.Execute();
        }

        public void GetPresentMediaDetails()
        {
        }

        public void UpdateMediaList(MediaDetailBObj mediaDetails)
        {
            _view.AddMedia(mediaDetails);
        }

        private class GetMediaDetailPresenterCallback : IGetMediaDetailPresenterCallback
        {
            private IShowMediaListViewModel _vm;

            public GetMediaDetailPresenterCallback(IShowMediaListViewModel vm) 
            {
                _vm = vm;
            }

            public void OnFailure(Exception exception)
            {
                Debug.WriteLine(exception);
            }

            public void OnSuccess(ZResponse<GetMediaDetailResponse> response)
            {
                _vm.UpdateMediaList(response.Data.MediaDetails);
            }
        }
    }
}