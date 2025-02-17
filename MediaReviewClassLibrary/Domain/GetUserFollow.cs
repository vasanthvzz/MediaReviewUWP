using CommonClassLibrary;
using MediaReviewClassLibrary.Data;
using MediaReviewClassLibrary.Models;
using MediaReviewClassLibrary.Models.Constants;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MediaReviewClassLibrary.Domain
{
    public class GetUserFollowUseCase : UseCaseBase<GetUserFollowResponse>
    {
        private GetUserFollowRequest _request;
        private IGetUserFollowDataManager _dm = MediaReviewDIServiceProvider.GetRequiredService<IGetUserFollowDataManager>();

        public GetUserFollowUseCase(GetUserFollowRequest request, ICallback<GetUserFollowResponse> callback) : base(callback)
        {
            _request = request;
        }

        public override void Action()
        {
            _dm.GetUserFollow(_request, new GetUserFollowUseCaseCallback(this));
        }
    }

    public class GetUserFollowUseCaseCallback : ICallback<GetUserFollowResponse>
    {
        private GetUserFollowUseCase _uc;

        public GetUserFollowUseCaseCallback(GetUserFollowUseCase uc)
        {
            _uc = uc;
        }

        public void OnFailure(Exception exception)
        {
            _uc?.PresenterCallback?.OnFailure(exception);
        }

        public void OnSuccess(ZResponse<GetUserFollowResponse> response)
        {
            _uc?.PresenterCallback?.OnSuccess(response);
        }
    }

    public class GetUserFollowRequest
    {
        public long UserId { get; set; }
        public FollowType UserFollowType { get; set; }
        public bool IsFollowing { get; set; }

        public GetUserFollowRequest(long userId, FollowType userFollowType, bool isFollowing = true)
        {
            UserId = userId;
            UserFollowType = userFollowType;
            IsFollowing = isFollowing;
        }
    }

    public class GetUserFollowResponse
    {
        public List<UserFollowBObj> FollowList { get; set; }

        public GetUserFollowResponse(List<UserFollowBObj> followList)
        {
            FollowList = followList;
        }
    }

    public interface IGetUserFollowDataManager
    {
        Task GetUserFollow(GetUserFollowRequest request, ICallback<GetUserFollowResponse> callback);
    }

    public interface IGetUserFollowPresenterCallback : ICallback<GetUserFollowResponse>
    { }
}