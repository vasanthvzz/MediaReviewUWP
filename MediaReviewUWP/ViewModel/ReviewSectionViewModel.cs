using CommonClassLibrary;
using MediaReviewClassLibrary;
using MediaReviewClassLibrary.Domain;
using MediaReviewClassLibrary.Models;
using MediaReviewClassLibrary.Models.Enitites;
using MediaReviewClassLibrary.Utlis;
using MediaReviewUWP.View.Contract;
using MediaReviewUWP.ViewModel.Contract;
using MediaReviewUWP.ViewObject;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace MediaReviewUWP.ViewModel
{
    public class ReviewSectionViewModel : IReviewSectionViewModel
    {
        private IManageReviewView _view;
        private ISessionManager _sessionManager = MediaReviewDIServiceProvider.GetServiceProvider().GetRequiredService<ISessionManager>();

        public ReviewSectionViewModel(IManageReviewView view)
        {
            _view = view;
        }

        #region View to View Model Communication

        public void GetMediaReviews(long mediaId)
        {
            IGetMediaReviewPresenterCallback callback = new GetMediaReviewPresenterCallback(this);
            GetMediaReviewRequest request = new GetMediaReviewRequest(mediaId, _sessionManager.RetriveUserFromStorage().UserId);
            GetMediaReviewUseCase uc = new GetMediaReviewUseCase(request, callback);
            uc.Execute();
        }

        public void AddReview(long mediaId, string description)
        {
            AddReviewPresenterCallback callback = new AddReviewPresenterCallback(this);
            var userId = _sessionManager.RetriveUserFromStorage().UserId;
            AddReviewRequest request = new AddReviewRequest(userId, mediaId, description);
            AddReviewUseCase usecase = new AddReviewUseCase(request, callback);
            usecase.Execute();
        }

        public void EditReview(long reviewId,long userId, string reviewContent)
        {
            EditReviewRequest request = new EditReviewRequest(reviewId , userId, reviewContent);
            EditReviewPresenterCallback callback = new EditReviewPresenterCallback(this);
            EditReviewUseCase uc = new EditReviewUseCase(request, callback);
            uc.Execute();
        }

        public void DeleteReview(long reviewId)
        {
            var userId = _sessionManager.RetriveUserFromStorage().UserId;
            DeleteReviewRequest request = new DeleteReviewRequest(reviewId,userId);
            DeleteReviewPresenterCallback callback = new DeleteReviewPresenterCallback(this);
            DeleteReviewUseCase uc = new DeleteReviewUseCase(request, callback);
            uc.Execute();
        }

        public void UpdateFollow(long followeeId,bool isFollowing)
        {
            var userId = _sessionManager.RetriveUserFromStorage().UserId ;
            UpdateFollowRequest request = new UpdateFollowRequest(userId, followeeId, isFollowing);
            UpdateFollowPresenterCallback callback = new UpdateFollowPresenterCallback(this);
            UpdateFollowUseCase uc = new UpdateFollowUseCase(request, callback);
            uc.Execute();
        }

        #endregion

        #region View model to View Communication

        public void SendMediaReviews(List<MediaReviewBObj> mediaReviews)
        {
            List<MediaReviewVObj> mediaReviewVObjs = new List<MediaReviewVObj>();
            foreach (var review in mediaReviews)
            {
                mediaReviewVObjs.Add(new MediaReviewVObj(review));
            }
            _view.UpdateMediaReviewList(mediaReviews);
        }

        public void SendEditedReview(Review updatedReview)
        {
            _view.UpdateExistingReview(updatedReview);
        }

        public void UpdateMediaReview(List<MediaReviewBObj> mediaReviews)
        {
            _view.UpdateMediaReviewList(mediaReviews);
        }

        public void UpdateMediaReview(MediaReviewBObj mediaReview)
        {
            _view.AddMediaReviewToList(mediaReview);
        }

        public void DeleteReviewInMediaList(Review review)
        {
            _view.DeleteReviewFromList(review);
        }

        public void OnFollowUpdate(long followeeId,bool isFollowing)
        {
            _view.ChangeFolloweeStatus(followeeId, isFollowing);
        }

        #endregion
    }

    public class EditReviewPresenterCallback : IEditReviewPresenterCallback
    {
        private IReviewSectionViewModel _vm;

        public EditReviewPresenterCallback(IReviewSectionViewModel vm) 
        { 
            _vm = vm;
        }

        public void OnFailure(Exception exception)
        {
            Debug.WriteLine(exception);
        }

        public void OnSuccess(ZResponse<EditReviewResponse> response)
        {
            _vm.SendEditedReview(response.Data.UpdatedReview);
        }
    }


    public class GetMediaReviewPresenterCallback : IGetMediaReviewPresenterCallback
    {
        private IReviewSectionViewModel _vm;

        public GetMediaReviewPresenterCallback(IReviewSectionViewModel vm)
        {
            _vm = vm;
        }

        public void OnFailure(Exception exception)
        {
            Debug.WriteLine(exception);
        }

        public void OnSuccess(ZResponse<GetMediaReviewResponse> response)
        {
            _vm.UpdateMediaReview(response.Data.MediaReviews);
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
                _viewModel.UpdateMediaReview(response.Data.UserReview);
            }
        }
    }

    public class DeleteReviewPresenterCallback : IDeleteReviewPresenterCallback
    {
        private IReviewSectionViewModel _viewModel;
        public DeleteReviewPresenterCallback(IReviewSectionViewModel viewModel)
        {
            _viewModel = viewModel;
        }

        public void OnFailure(Exception exception)
        {
            Debug.WriteLine(exception);
        }

        public void OnSuccess(ZResponse<DeleteReviewResponse> response)
        {
            _viewModel.DeleteReviewInMediaList(response.Data.DeletedReview);
        }
    }

    public class UpdateFollowPresenterCallback : IUpdateFollowPresenterCallback
    {
        private IReviewSectionViewModel _viewModel;
        public UpdateFollowPresenterCallback(IReviewSectionViewModel viewModel)
        {
            _viewModel = viewModel;
        }

        public void OnFailure(Exception exception)
        {
            Debug.WriteLine(exception);
        }

        public void OnSuccess(ZResponse<UpdateFollowResponse> response)
        {
            FolloweeMapper updatedFollow = response.Data.UpdatedFollow;
            _viewModel.OnFollowUpdate(updatedFollow.FolloweeId, updatedFollow.IsFollowing);
        }
    }

}
