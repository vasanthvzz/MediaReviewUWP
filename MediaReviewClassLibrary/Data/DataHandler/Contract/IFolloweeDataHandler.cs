using MediaReviewClassLibrary.Models.Enitites;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MediaReviewClassLibrary.Data.DataHandler.Contract
{
    public interface IFolloweeDataHandler
    {
        Task<FolloweeMapper> AddFollowee(long userId, long followeeId);

        Task AddFollowee(FolloweeMapper followeeDetail);

        Task<FolloweeMapper> GetFollowingDetail(long userId, long followeeId);

        Task<List<FolloweeMapper>> GetUserFollowee(long userId);

        Task<List<FolloweeMapper>> GetUserFollower(long userId);

        Task<FolloweeMapper> UpdateFollowee(FolloweeMapper followeeDetail);
    }
}