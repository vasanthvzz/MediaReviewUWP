using CommonClassLibrary;
using MediaReviewClassLibrary;
using MediaReviewClassLibrary.Domain;
using MediaReviewClassLibrary.Models;
using MediaReviewClassLibrary.Models.Enitites;
using MediaReviewClassLibrary.Utlis;
using MediaReviewUWP.View.Contract;
using MediaReviewUWP.View.MediaPageView;
using MediaReviewUWP.ViewModel.Contract;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Diagnostics;
using static MediaReviewClassLibrary.Domain.GetMediaDetail;

namespace MediaReviewUWP.ViewModel
{
    public class MediaPageViewModel : IMediaPageViewModel
    {
        private ISessionManager _sessionManager = MediaReviewDIServiceProvider.GetServiceProvider().GetRequiredService<ISessionManager>();
        private IMediaPage _view;

        public MediaPageViewModel(IMediaPage mediaPage)
        {
            _view = mediaPage;
        }

        #region Fetch Media Details
        public void GetMediaDetail(long mediaId)
        {
            UserDetail userDetail = _sessionManager.RetriveUserFromStorage();
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
            private MediaPageViewModel _vm;
            public GetMediaPresenterCallback(MediaPageViewModel vm)
            {
                _vm = vm;
            }

            public void OnSuccess(ZResponse<GetMediaDetailResponse> response)
            {
                _vm.SendMediaDetail(response.Data.MediaDetails);
            }

            public void OnFailure(Exception exception)
            {
                Debug.WriteLine(exception);
            }
        }
        #endregion
    }
}
