using CommonClassLibrary;
using MediaReviewClassLibrary;
using MediaReviewClassLibrary.Data;
using MediaReviewClassLibrary.Domain;
using MediaReviewClassLibrary.Models;
using MediaReviewClassLibrary.Models.Enitites;
using MediaReviewClassLibrary.Utility;
using MediaReviewUWP.View.Contract;
using MediaReviewUWP.ViewModel.Contract;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Diagnostics;

namespace MediaReviewUWP.ViewModel
{
    public class MediaPageViewModel : IMediaPageViewModel
    {
        private IMediaPage _view;

        public MediaPageViewModel(IMediaPage mediaPage)
        {
            _view = mediaPage;
        }

        #region Fetch Media Details

        public void GetMediaDetail(long mediaId)
        {
            UserDetail userDetail = SessionManager.User;
            GetMediaDetailRequest request = new GetMediaDetailRequest(userDetail.UserId, mediaId);
            IGetMediaDetailPresenterCallback callback = new GetMediaPresenterCallback(this);
            GetMediaDetailUseCase uc = new GetMediaDetailUseCase(request, callback);
            uc.Execute();
        }

        public void SendMediaDetail(MediaDetailBObj mediaDetail)
        {
            _view.UpdateMediaPage(mediaDetail);
        }

        private class GetMediaPresenterCallback : IGetMediaDetailPresenterCallback
        {
            private IMediaPageViewModel _vm;

            public GetMediaPresenterCallback(MediaPageViewModel vm)
            {
                _vm = vm;
            }

            public void OnSuccess(ZResponse<GetMediaDetailResponse> response)
            {
                _vm?.SendMediaDetail(response.Data.MediaDetails);
            }

            public void OnFailure(Exception exception)
            {
                Debug.WriteLine(exception);
            }
        }

        #endregion Fetch Media Details
    }
}