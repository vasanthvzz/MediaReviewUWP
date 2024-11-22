using MediaReviewClassLibrary.Data.Contract;
using MediaReviewClassLibrary.Data.DataHandler.Contract;
using MediaReviewClassLibrary.Models.Enitites;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace MediaReviewClassLibrary.Data.DataHandler
{
    public class RatingDataHandler : IRatingDataHandler
    {
        private IDatabaseAdapter _databaseAdapter = MediaReviewDIServiceProvider.GetServiceProvider().GetService<IDatabaseAdapter>();

        public async Task<Rating> GetUserRating(long userId, long mediaId)
        {
            var tabledQuery = _databaseAdapter.GetTableQuery<Rating>();
            return await tabledQuery.Where(rating => rating.UserId == userId && rating.MediaId == mediaId).FirstOrDefaultAsync();
        }

        public async Task<Rating> UpdateUserRating(Rating userRating)
        {
            var existingRecord = await GetUserRating(userRating.UserId, userRating.MediaId);

            if (existingRecord == null)
            {
                await _databaseAdapter.InsertAsync<Rating>(userRating);
            }
            else
            {
                // Execute update query asynchronously
                var customQuery = "UPDATE rating SET score = ? WHERE media_id = ? AND user_id = ?";
                await _databaseAdapter.ExecuteQuery<Rating>(customQuery, userRating.Score, userRating.MediaId, userRating.UserId);
            }
            return await GetUserRating(userRating.UserId, userRating.MediaId);  
        }
    }
}
