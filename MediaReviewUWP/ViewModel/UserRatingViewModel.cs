using CommonClassLibrary;
using MediaReviewClassLibrary.Domain;
using MediaReviewClassLibrary.Models.Enitites;
using MediaReviewUWP.View.Contract;
using MediaReviewUWP.ViewModel.Contract;
using System;
using System.Diagnostics;

namespace MediaReviewUWP.ViewModel
{
    public class UserRatingViewModel : IUserRatingViewModel
    {
        private IUserRatingView _view;

        public UserRatingViewModel(IUserRatingView view)
        {
            _view = view;
        }

        public void UpdateUserRating(Rating userRating)
        {
            UpdateUserRatingPresenterCallback callback = new UpdateUserRatingPresenterCallback(this);
            UpdateUserRatingRequest request = new UpdateUserRatingRequest(userRating);
            UpdateUserRatingUseCase uc = new UpdateUserRatingUseCase(request, callback);
            uc.Execute();
        }

        public void SendUserRating(Rating userRating)
        {
            _view.UpdatedUserRating(userRating);
        }
    }

    public class UpdateUserRatingPresenterCallback : IUpdateUserRatingPresenterCallback
    {
        private IUserRatingViewModel _vm;

        public UpdateUserRatingPresenterCallback(IUserRatingViewModel vm)
        {
            _vm = vm;
        }

        public void OnSuccess(ZResponse<UpdateUserRatingResponse> response)
        {
            _vm.SendUserRating(response.Data.UserRating);
        }

        public void OnFailure(Exception exception)
        {
            Debug.WriteLine(exception);
        }
    }
}