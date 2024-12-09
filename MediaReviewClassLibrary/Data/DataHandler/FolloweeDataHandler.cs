using MediaReviewClassLibrary.Data.Contract;
using MediaReviewClassLibrary.Data.DataHandler.Contract;
using MediaReviewClassLibrary.Models.Enitites;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MediaReviewClassLibrary.Data.DataHandler
{
    public class FolloweeDataHandler : IFolloweeDataHandler
    {
        private IDatabaseAdapter _databaseAdapter = MediaReviewDIServiceProvider.GetServiceProvider().GetRequiredService<IDatabaseAdapter>();

        public async Task<FolloweeMapper> AddFollowee(long userId, long followeeId)
        {
            FolloweeMapper followeeMapper = new FolloweeMapper(userId, followeeId);
            await _databaseAdapter.InsertAsync<FolloweeMapper>(followeeMapper);
            return followeeMapper;
        }

        public async Task AddFollowee(FolloweeMapper followeeDetail)
        {
            await _databaseAdapter.InsertAsync(followeeDetail);
        }

        public async Task<FolloweeMapper> GetFollowingDetail(long userId, long followeeId)
        {
            if(userId == followeeId)
            {
                throw new Exception("User id and followee id are same. Cannot perform follow");
            }
            return await _databaseAdapter.GetTableQuery<FolloweeMapper>()
                .Where(followerMapper => followerMapper.UserId == userId && followerMapper.FolloweeId == followeeId)
                .FirstOrDefaultAsync();
        }

        public async Task<List<FolloweeMapper>> GetUserFollowee(long userId)
        {
            return await _databaseAdapter.GetTableQuery<FolloweeMapper>()
                .Where(followerMapper => followerMapper.UserId == userId && followerMapper.IsFollowing).ToListAsync();
        }

        public async Task<List<FolloweeMapper>> GetUserFollower(long userId)
        {
            return await _databaseAdapter.GetTableQuery<FolloweeMapper>()
                .Where(followerMapper => followerMapper.FolloweeId == userId && followerMapper.IsFollowing).ToListAsync();
        }

        public async Task<FolloweeMapper> UpdateFollowee(FolloweeMapper followeeDetail)
        {
            if (followeeDetail.UserId == followeeDetail.FolloweeId)
            {
                throw new Exception("User id and followee id are same. Cannot perform follow");
            }

            var customQuery = "UPDATE followee_mapper SET is_following = ? WHERE user_id = ? AND followee_id = ?";
            await _databaseAdapter.ExecuteQuery<FolloweeMapper>(customQuery, followeeDetail.IsFollowing, followeeDetail.UserId, followeeDetail.FolloweeId);
            return await GetFollowingDetail(followeeDetail.UserId,followeeDetail.FolloweeId);
        }
    }
}