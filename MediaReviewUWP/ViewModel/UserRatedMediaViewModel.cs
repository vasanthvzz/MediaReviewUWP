using CommonClassLibrary;
using MediaReviewClassLibrary;
using MediaReviewClassLibrary.Data;
using MediaReviewClassLibrary.Domain;
using MediaReviewClassLibrary.Models;
using MediaReviewClassLibrary.Models.Enitites;
using MediaReviewClassLibrary.Utility;
using MediaReviewUWP.View.Contract;
using MediaReviewUWP.ViewModel.Contract;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace MediaReviewUWP.ViewModel
{
    public class UserRatedMediaViewModel : IUserRatedMediaViewModel
    {
        private IUserRatedMediaPage _view;

        public UserRatedMediaViewModel(IUserRatedMediaPage view)
        {
            _view = view;
        }

        public void ChangeUserRating(long mediaId, short userRating)
        {
            long userId = SessionManager.User.UserId;
            UpdateUserRatingRequest request = new UpdateUserRatingRequest(new Rating(userId, mediaId, userRating));
            UpdateUserRatingPresenterCallback callback = new UpdateUserRatingPresenterCallback(this);
            UpdateUserRatingUseCase uc = new UpdateUserRatingUseCase(request, callback);
            uc.Execute();
        }

        public void GetUserRatedMedia()
        {
            long userId = SessionManager.User.UserId;
            GetUserRatedMediaRequest request = new GetUserRatedMediaRequest(userId);
            GetUserRatedMediaPresenterCallback callback = new GetUserRatedMediaPresenterCallback(this);
            GetUserRatedMediaUseCase uc = new GetUserRatedMediaUseCase(request, callback);
            uc.Execute();
        }

        public void SendRatedMedia(List<UserRatingBObj> ratedMedia)
        {
            _view.UpdateRatedMediaList(ratedMedia);
        }

        public void SendUpdatedRating(Rating rating)
        {
            _view.UpdatedMediaRating(rating);
        }

        private class GetUserRatedMediaPresenterCallback : IGetUserRatedMediaPresenterCallback
        {
            private IUserRatedMediaViewModel _vm;

            public GetUserRatedMediaPresenterCallback(IUserRatedMediaViewModel vm)
            {
                _vm = vm;
            }

            public void OnFailure(Exception exception)
            {
                Debug.WriteLine(exception);
            }

            public void OnSuccess(ZResponse<GetUserRatedMediaResponse> response)
            {
                _vm?.SendRatedMedia(response.Data.RatedMedia);
            }
        }

        private class UpdateUserRatingPresenterCallback : IUpdateUserRatingPresenterCallback
        {
            private IUserRatedMediaViewModel _vm;

            public UpdateUserRatingPresenterCallback(IUserRatedMediaViewModel vm)
            {
                _vm = vm;
            }

            public void OnFailure(Exception exception)
            {
                Debug.WriteLine(exception);
            }

            public void OnSuccess(ZResponse<UpdateUserRatingResponse> response)
            {
                if (response.Data.Success)
                {
                    _vm.SendUpdatedRating(response.Data.UserRating);
                }
            }
        }
    }
}