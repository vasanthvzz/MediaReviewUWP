using CommonClassLibrary;
using MediaReviewClassLibrary;
using MediaReviewClassLibrary.Domain;
using MediaReviewClassLibrary.Models;
using MediaReviewClassLibrary.Models.Enitites;
using MediaReviewClassLibrary.Utlis;
using MediaReviewUWP.View.Contract;
using MediaReviewUWP.ViewModel.Contract;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Diagnostics;

namespace MediaReviewUWP.ViewModel
{
    public class ReviewSectionViewModel : IReviewSectionViewModel
    {
        private IReviewSectionView _view;
        private ISessionManager _sessionManager = MediaReviewDIServiceProvider.GetServiceProvider().GetRequiredService<ISessionManager>();  

        public ReviewSectionViewModel(IReviewSectionView view)
        {
            _view = view;
        }

        public void AddReview(long mediaId,string description)
        {
            AddReviewPresenterCallback callback = new AddReviewPresenterCallback(this);
            var userId = _sessionManager.RetriveUserFromStorage().UserId;
            AddReviewRequest request = new AddReviewRequest(userId,mediaId, description);
            AddReviewUseCase usecase = new AddReviewUseCase(request,callback);
            usecase.Execute();
        }

        public void SendReviewUpdate(MediaReviewBObj review)
        {
            _view.OnReviewAdded(review);
        }
    }

    public class AddReviewPresenterCallback : IAddReviewPresenterCallback
    {
        private IReviewSectionViewModel _viewModel;

        public AddReviewPresenterCallback(IReviewSectionViewModel viewModel)
        {
            _viewModel = viewModel;
        }

        public void OnFailure(Exception exception)
        {
            Debug.WriteLine(exception);
        }

        public void OnSuccess(ZResponse<AddReviewResponse> response)
        {
            if (response.Data.IsSuccess)
            {
                _viewModel.SendReviewUpdate(response.Data.UserReview);
            }
        }
    }
}
