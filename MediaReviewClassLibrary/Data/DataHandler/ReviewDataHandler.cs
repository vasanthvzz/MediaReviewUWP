using MediaReviewClassLibrary.Data.Contract;
using MediaReviewClassLibrary.Data.DataHandler.Contract;
using MediaReviewClassLibrary.Models.Enitites;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MediaReviewClassLibrary.Data.DataHandler
{
    public class ReviewDataHandler : IReviewDataHandler
    {
        private IDatabaseAdapter _databaseAdapter = MediaReviewDIServiceProvider.GetServiceProvider().GetService<IDatabaseAdapter>();

        public void AddReview(Review review)
        {
            _databaseAdapter.InsertAsync(review);
        }

        public async Task<List<Review>> GetReviewsByMedia(long mediaId)
        {
            var tabledQuery = _databaseAdapter.GetTableQuery<Review>();
            return await tabledQuery.Where(review => review.MediaId == mediaId).ToListAsync();
        }
    }
}
