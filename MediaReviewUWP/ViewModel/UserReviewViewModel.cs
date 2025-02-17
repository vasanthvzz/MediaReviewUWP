using CommonClassLibrary;
using MediaReviewClassLibrary;
using MediaReviewClassLibrary.Data;
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
    public class UserReviewViewModel : IUserReviewViewModel
    {
        private IUserReviewPage _view;

        public UserReviewViewModel(IUserReviewPage view)
        {
            _view = view;
        }

        public void DeleteReview(long reviewId)
        {
            long userId = SessionManager.User.UserId;
            DeleteReviewRequest request = new DeleteReviewRequest(reviewId, userId);
            DeleteReviewPresenterCallback callback = new DeleteReviewPresenterCallback(this);
            DeleteReviewUseCase uc = new DeleteReviewUseCase(request, callback);
            uc.Execute();
        }

        public void EditReview(long reviewId, string reviewContent)
        {
            long userId = SessionManager.User.UserId;
            EditReviewRequest request = new EditReviewRequest(reviewId, userId, reviewContent);
            EditReviewPresenterCallback callback = new EditReviewPresenterCallback(this);
            EditReviewUseCase uc = new EditReviewUseCase(request, callback);
            uc.Execute();
        }

        public void GetUserReviews()
        {
            long userId = SessionManager.User.UserId;
            GetUserReviewPresenterCallback callback = new GetUserReviewPresenterCallback(this);
            GetUserReviewRequest request = new GetUserReviewRequest(userId);
            GetUserReviewUseCase uc = new GetUserReviewUseCase(request, callback);
            uc.Execute();
        }

        public void UpdateUserReview(List<UserReviewBObj> userReviews)
        {
            _view.UpdateUserReviews(userReviews);
        }

        public void SendDeletedReview(Review deletedReview)
        {
            _view.DeleteReview(deletedReview);
        }

        public void SendEditedReview(Review updatedReview)
        {
            _view.UpdateExistingReview(updatedReview);
        }

        private class GetUserReviewPresenterCallback : IGetUserReviewPresenterCallback
        {
            private IUserReviewViewModel _vm;

            public GetUserReviewPresenterCallback(IUserReviewViewModel vm)
            {
                _vm = vm;
            }

            public void OnFailure(Exception exception)
            {
                Debug.WriteLine(exception);
            }

            public void OnSuccess(ZResponse<GetUserReviewResponse> response)
            {
                _vm.UpdateUserReview(response.Data.UserReviews);
            }
        }

        private class EditReviewPresenterCallback : IEditReviewPresenterCallback
        {
            private IUserReviewViewModel _vm;

            public EditReviewPresenterCallback(IUserReviewViewModel vm)
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

        private class DeleteReviewPresenterCallback : IDeleteReviewPresenterCallback
        {
            private IUserReviewViewModel _vm;

            public DeleteReviewPresenterCallback(IUserReviewViewModel vm)
            {
                _vm = vm;
            }

            public void OnFailure(Exception exception)
            {
                Debug.WriteLine(exception);
            }

            public void OnSuccess(ZResponse<DeleteReviewResponse> response)
            {
                if (response.Data.Success)
                {
                    _vm.SendDeletedReview(response.Data.DeletedReview);
                }
            }
        }
    }
}