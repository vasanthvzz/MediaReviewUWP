using CommonClassLibrary;
using MediaReviewClassLibrary.Data.DataHandler.Contract;
using MediaReviewClassLibrary.Domain;
using MediaReviewClassLibrary.Models;
using MediaReviewClassLibrary.Models.Constants;
using MediaReviewClassLibrary.Models.Enitites;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MediaReviewClassLibrary.DataManager
{
    public class GetUserFollowDataManager : IGetUserFollowDataManager
    {
        private IUserDataHandler _userDataHandler = MediaReviewDIServiceProvider.GetRequiredService<IUserDataHandler>();
        private IFolloweeDataHandler _followeeDataHandler = MediaReviewDIServiceProvider.GetRequiredService<IFolloweeDataHandler>();

        public async Task GetUserFollow(GetUserFollowRequest request, ICallback<GetUserFollowResponse> callback)
        {
            try
            {
                var userId = request.UserId;
                List<FolloweeMapper> followList = new List<FolloweeMapper>();
                List<UserFollowBObj> userFollowList = new List<UserFollowBObj>();
                if (request.UserFollowType == FollowType.FOLLOWER)
                {
                    followList = await _followeeDataHandler.GetUserFollower(userId);
                    foreach (var follow in followList)
                    {
                        UserDetail user = await _userDataHandler.GetUserById(follow.UserId);
                        userFollowList.Add(new UserFollowBObj(user, follow,FollowType.FOLLOWER));
                    }
                }
                else
                {
                    followList = await _followeeDataHandler.GetUserFollowee(userId);
                    foreach (var follow in followList)
                    {
                        UserDetail user = await _userDataHandler.GetUserById(follow.FolloweeId);
                        userFollowList.Add(new UserFollowBObj(user, follow,FollowType.FOLLOWEE));
                    }
                }
                ZResponse<GetUserFollowResponse> response = new ZResponse<GetUserFollowResponse>(new GetUserFollowResponse(userFollowList));
                callback?.OnSuccess(response);
            }
            catch (Exception e)
            {
                callback?.OnFailure(e);
            }
        }
    }
}