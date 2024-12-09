using MediaReviewClassLibrary.Data.Contract;
using MediaReviewClassLibrary.Data.DataHandler.Contract;
using MediaReviewClassLibrary.Models.Enitites;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MediaReviewClassLibrary.Data.DataHandler
{
    public class RatingDataHandler : IRatingDataHandler
    {
        private IDatabaseAdapter _databaseAdapter = MediaReviewDIServiceProvider.GetServiceProvider().GetService<IDatabaseAdapter>();

        public async Task<float> GetAverageRating(long mediaId)
        {
            var tabledQuery = _databaseAdapter.GetTableQuery<Rating>();
            var totalRatings = await tabledQuery.Where(rating => rating.MediaId == mediaId && rating.Score >= 1).ToListAsync();
            float ratingCount =  totalRatings.Sum(rating => rating.Score);
            if(ratingCount == 0)
            {
                return 0;
            }
            return ratingCount / totalRatings.Count;
        }

        public async Task<long> GetRatedUserCount(long mediaId)
        {
            var tabledQuery = _databaseAdapter.GetTableQuery<Rating>();
            return  await tabledQuery.Where(rating => rating.MediaId == mediaId && rating.Score >= 1).CountAsync();
        }

        public async Task<Rating> GetUserRating(long userId, long mediaId)
        {
            var tabledQuery = _databaseAdapter.GetTableQuery<Rating>();
            return await tabledQuery.Where(rating => rating.UserId == userId && rating.MediaId == mediaId).FirstOrDefaultAsync();
        }

        public async Task<List<Rating>> GetAllUserRating(long userId)
        {
            var tabledQuery = _databaseAdapter.GetTableQuery<Rating>();
            return await tabledQuery.Where(rating => rating.UserId == userId && rating.Score >= 1).ToListAsync();
        }

        public async Task<Rating> UpdateUserRating(Rating userRating)
        {
            var existingRecord = await GetUserRating(userRating.UserId, userRating.MediaId);

            if (existingRecord == null)
            {
                await _databaseAdapter.InsertAsync<Rating>(userRating);
                return userRating;
            }
            else
            {
                var customQuery = "UPDATE rating SET score = ? WHERE media_id = ? AND user_id = ?";
                await _databaseAdapter.ExecuteQuery<Rating>(customQuery, userRating.Score, userRating.MediaId, userRating.UserId);
                return await GetUserRating(userRating.UserId, userRating.MediaId);
            }
        }
    }
}
