using CommonClassLibrary;
using MediaReviewClassLibrary.Domain;
using MediaReviewUWP.View.Contract;
using MediaReviewUWP.ViewModel.Contract;
using System;
using System.Diagnostics;

namespace MediaReviewUWP.ViewModel
{
    public class MediaDetailViewModel : IMediaDetailViewModel
    {
        private IMediaDetailControl _view;

        public MediaDetailViewModel(IMediaDetailControl view)
        {
            _view = view;
        }

        public void SendUpdatedMediaRating(float MediaRating)
        {
            _view.UpdateMediaRating(MediaRating);
        }

        public void GetMediaRating(long mediaId)
        {
            GetMediaRatingRequest request = new GetMediaRatingRequest(mediaId);
            GetMediaRatingPresenterCallback callback = new GetMediaRatingPresenterCallback(this);
            GetMediaRatingUseCase uc = new GetMediaRatingUseCase(request, callback);
            uc.Execute();
        }

        private class GetMediaRatingPresenterCallback : IGetMediaRatingPresenterCallback
        {
            private IMediaDetailViewModel _vm;

            public GetMediaRatingPresenterCallback(IMediaDetailViewModel vm)
            {
                _vm = vm;
            }

            public void OnFailure(Exception exception)
            {
                Debug.WriteLine(exception);
            }

            public void OnSuccess(ZResponse<GetMediaRatingResponse> response)
            {
                _vm?.SendUpdatedMediaRating(response.Data.RatingScore);
            }
        }
    }
}