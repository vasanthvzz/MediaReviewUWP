using CommonClassLibrary;
using MediaReviewClassLibrary.Data.DataHandler.Contract;
using MediaReviewClassLibrary.Domain;
using MediaReviewClassLibrary.Models.Enitites;
using System;
using System.Threading.Tasks;

namespace MediaReviewClassLibrary.DataManager
{
    public class UpdateFollowDataManager : IUpdateFollowDataManager
    {
        private IFolloweeDataHandler _followerDataHandler = MediaReviewDIServiceProvider.GetRequiredService<IFolloweeDataHandler>();

        public async Task<bool> GetFollowingStatus(long userId, long followeeId)
        {
            if(userId == followeeId)
            {
                return false;
            }
            var followeeMapper = await _followerDataHandler.GetFollowingDetail(userId, followeeId);
            if(followeeMapper == null)
            {
                return false;
            }
            return followeeMapper.IsFollowing;
        }

       public async Task UpdateFollow(UpdateFollowRequest request , UpdateFollowUseCaseCallback callback)
        {
            try
            {
                if(request.UserId == request.FolloweeId)
                {
                    callback?.OnFailure(new Exception("Cannot assign follow to the same user"));
                }
                var followeeDetail = await _followerDataHandler.GetFollowingDetail(request.UserId, request.FolloweeId);
                FolloweeMapper updatedFollowee;
                if(followeeDetail == null)
                {
                    followeeDetail = new FolloweeMapper(request.UserId,request.FolloweeId,request.IsFollowing);
                    await _followerDataHandler.AddFollowee(followeeDetail);
                    updatedFollowee = followeeDetail;
                }
                else
                {
                    followeeDetail.IsFollowing = request.IsFollowing;
                    updatedFollowee = await _followerDataHandler.UpdateFollowee(followeeDetail);
                }
                ZResponse<UpdateFollowResponse> response = new ZResponse<UpdateFollowResponse>(new UpdateFollowResponse(updatedFollowee));
                callback?.OnSuccess(response);
            }
            catch(Exception e)
            {
                callback?.OnFailure(e);
            }
        }
    }
}
