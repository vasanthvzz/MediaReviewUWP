using CommonClassLibrary;
using MediaReviewClassLibrary;
using MediaReviewClassLibrary.Data;
using MediaReviewClassLibrary.Domain;
using MediaReviewClassLibrary.Models;
using MediaReviewClassLibrary.Models.Constants;
using MediaReviewClassLibrary.Models.Enitites;
using MediaReviewUWP.View.Contract;
using MediaReviewUWP.ViewModel.Contract;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace MediaReviewUWP.ViewModel
{
    public class FollowListViewModel : IFollowListViewModel
    {
        private IFollowListControlView _view;

        public FollowListViewModel(IFollowListControlView view)
        {
            _view = view;
        }

        public void ChangeFollow(long targetId, bool isFollow, FollowType followType)
        {
            UpdateFollowRequest request;
            if (followType == FollowType.FOLLOWER)
            {
                long userId = targetId;
                long followeeId = SessionManager.User.UserId;
                request = new UpdateFollowRequest(userId, followeeId, isFollow);
            }
            else
            {
                long followeeId = targetId;
                long userId = SessionManager.User.UserId;
                request = new UpdateFollowRequest(userId, followeeId, isFollow);
            }
            UpdateFollowPresenterCallback callback = new UpdateFollowPresenterCallback(this);
            UpdateFollowUseCase uc = new UpdateFollowUseCase(request, callback);
            uc.Execute();
        }

        public void GetUserFollow(FollowType followType)
        {
            long userId = SessionManager.User.UserId;
            GetUserFollowRequest request = new GetUserFollowRequest(userId, followType);
            GetUserFollowPresenterCallback callback = new GetUserFollowPresenterCallback(this);
            GetUserFollowUseCase uc = new GetUserFollowUseCase(request,callback);
            uc.Execute();
        }

        public void UpdateFollow(FolloweeMapper updatedFollow)
        {
            _view.UpdateFollow(updatedFollow);
        }

        public void UpdateFollowList(List<UserFollowBObj> followList)
        {
            _view.UpdateFollow(followList);
        }

        private class UpdateFollowPresenterCallback : IUpdateFollowPresenterCallback
        {
            private IFollowListViewModel _vm;

            public UpdateFollowPresenterCallback(IFollowListViewModel vm)
            {
                _vm = vm;
            }
            
            public void OnFailure(Exception e)
            {
                Debug.WriteLine(e);
            }

            public void OnSuccess(ZResponse<UpdateFollowResponse> response)
            {
                _vm?.UpdateFollow(response.Data.UpdatedFollow);
            }
        }
    }




    public class GetUserFollowPresenterCallback : IGetUserFollowPresenterCallback
    {
        private IFollowListViewModel _vm;

        public GetUserFollowPresenterCallback(IFollowListViewModel vm)
        {
            _vm = vm;
        }

        public void OnFailure(Exception e)
        {
            Debug.WriteLine(e);
        }

        public void OnSuccess(ZResponse<GetUserFollowResponse> response)
        {
            _vm?.UpdateFollowList(response.Data.FollowList);
        }
    }
}
