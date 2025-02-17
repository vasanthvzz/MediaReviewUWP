using MediaReviewClassLibrary.Data.Contract;
using MediaReviewClassLibrary.Data.DataHandler.Contract;
using MediaReviewClassLibrary.Models.Enitites;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MediaReviewClassLibrary.Data.DataHandler
{
    public class ReviewDataHandler : IReviewDataHandler
    {
        private IDatabaseAdapter _databaseAdapter = MediaReviewDIServiceProvider.GetRequiredService<IDatabaseAdapter>();

        public void AddReview(Review review)
        {
            _databaseAdapter.InsertAsync(review);
        }

        public Task DeleteReview(long reviewId)
        {
            return _databaseAdapter.DeleteAsync<Review>(reviewId);
        }

        public async Task<List<Review>> GetAllUserReviews(long userId)
        {
            var tabledQuery = _databaseAdapter.GetAsyncTableQuery<Review>();
            return await tabledQuery.Where(review => review.UserId == userId).ToListAsync();
        }

        public async Task<Review> GetReviewById(long reviewId)
        {
            return await _databaseAdapter.FindAsync<Review>(reviewId);
        }

        public async Task<List<Review>> GetReviewsByMedia(long mediaId)
        {
            var tabledQuery = _databaseAdapter.GetAsyncTableQuery<Review>();
            return await tabledQuery.Where(review => review.MediaId == mediaId).ToListAsync();
        }

        public async Task<Review> UpdateReview(long reviewId, string reviewContent)
        {
            var customQuery = "UPDATE review SET description = ? , is_edited = ? WHERE review_id = ?";
            await _databaseAdapter.ExecuteQuery<Review>(customQuery, reviewContent, true, reviewId);
            return await _databaseAdapter.FindAsync<Review>(reviewId);
        }
    }
}