using CommonClassLibrary;
using MediaReviewClassLibrary.Domain;
using MediaReviewClassLibrary.Models;
using MediaReviewUWP.View.Contract;
using MediaReviewUWP.View.MediaPageView;
using MediaReviewUWP.ViewModel.Contract;
using MediaReviewUWP.ViewObjects;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace MediaReviewUWP.ViewModel
{
    public class ReviewListViewModel : IReviewListViewModel
    {
        private IReviewListView _view;

        public ReviewListViewModel(IReviewListView view)
        {
            _view = view;
        }

        public void GetMediaReviews(long mediaId)
        {
            IGetMediaReviewPresenterCallback callback = new GetMediaReviewPresenterCallback(this);
            GetMediaReviewRequest request = new GetMediaReviewRequest(mediaId);
            GetMediaReviewUseCase uc = new GetMediaReviewUseCase(request, callback);    
            uc.Execute();
        }

        public void SendMediaReviews(List<MediaReviewBObj> mediaReviews)
        {
            List<MediaReviewVObj> mediaReviewVObjs = new List<MediaReviewVObj>();
            foreach (var review in mediaReviews) 
            { 
                mediaReviewVObjs.Add(new MediaReviewVObj(review));
            }
            _view.UpdateMediaReviews(mediaReviewVObjs);
        }
    }

    public class GetMediaReviewPresenterCallback : IGetMediaReviewPresenterCallback
    {
        private IReviewListViewModel _vm;

        public GetMediaReviewPresenterCallback(IReviewListViewModel vm)
        {
            _vm = vm;
        }

        public void OnFailure(Exception exception)
        {
            Debug.WriteLine(exception);
        }

        public void OnSuccess(ZResponse<GetMediaReviewResponse> response)
        {
            _vm.SendMediaReviews(response.Data.MediaReviews);
        }
    }
}
